using WebSocketSharp;
using UnityEngine;
using UnityEngine.UI;

public class WebSocketContoller : MonoBehaviour
{
    [SerializeField] private TextChanger textChanger;


    [SerializeField] private TrowingAxeDetection trowingAxeDetection;
    [SerializeField] private AxeAttempts axeAttempts;
    [SerializeField] private RoundsController roundsController;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private InputField ipAddressInputField;
    private WebSocket _webSocket;
    private AxeJsonParser _jsonParser;
    [SerializeField] private PanelChanger panelChanger;

     private ManualConfigurationSwitch _manualConfigurationSwitch;
    [SerializeField] private UserController userController;
    [SerializeField] private GameTypeChanger gameTypeChanger;


    [SerializeField] private GameObject defaultTarget;
    [SerializeField] private GameObject shadowAxeTarget;

    private FromJsonAxeWebsocket _axeWebsocket;
    private FromJsonUserWebsocket _userWebsocket;
    private FromJsonGameInfoWebsocket _gameInfoWebsocket;
    private FromJsonGottenScoreWebsocket _gottenScoreWebsocket;
    private bool _axeMessageReceived;
    private bool _userMessageReceived;
    private bool _gottenScoreMessageReceived;
    private bool _gameInfoMessageReceived;
    private bool _onConnected;
    private bool _onClosed;
    private bool _isPlayfieldState;
    private bool _isNeedToTurnOffPlayfield;

    private void Start()
    {
        _manualConfigurationSwitch = GetComponent<ManualConfigurationSwitch>();
        _jsonParser = GetComponent<AxeJsonParser>();

        ipAddressInputField.text = GetLocalIPAddress();
    }

    public static string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        return "0.0.0.0";
    }

    public void ConnectWebSocket()
    {
        _webSocket = new WebSocket("ws://" + ipAddressInputField.text + ":8000/ws/123");

        _webSocket.Log.Output = (data, s) =>
        {
              Debug.Log(data.ToString());

        };
        _webSocket.OnMessage += (sender, e) =>
        {
            Debug.Log("OnMessage");
            Debug.Log(e.Data);
            var newAxeWebsocket = _jsonParser.ParseRaspberryPieString(e.Data);
            if (!newAxeWebsocket.IsDefault())
            {            
                _axeWebsocket = newAxeWebsocket;
                _axeMessageReceived = true;
            }
            var newUserWebsocket = _jsonParser.ParseUserString(e.Data);
            if (!newUserWebsocket.IsDefault())
            {            
                _userWebsocket = newUserWebsocket;
                _userMessageReceived = true;
            }            
            var newTrowsCount = _jsonParser.ParseGameInfoString(e.Data);
            if (!newTrowsCount.IsDefault())
            {            
                _gameInfoWebsocket = newTrowsCount;
                _gameInfoMessageReceived = true;
            }            
            var newGottenScore = _jsonParser.ParseGottenScoreString(e.Data);
            if (!newGottenScore.IsDefault())
            {            
                _gottenScoreWebsocket = newGottenScore;
                
                _gottenScoreMessageReceived = true;
            }

            if (!_isPlayfieldState && e.Data.Contains("PLAYFIELD"))
            {
                _isPlayfieldState = true;
            } else if (_isPlayfieldState && e.Data.Contains("PLAYFIELD"))
            {
                _isPlayfieldState = false;
                _isNeedToTurnOffPlayfield = true;
            }

        };
        _webSocket.OnOpen += (sender, e) =>
        {
            Debug.Log("OnOpen");
            _onConnected = true;
        };
        _webSocket.OnClose += (sender, e) =>
        {
            Debug.Log("OnClose");
            Debug.Log(e.Reason);
            _onClosed = true;
        };
        _webSocket.ConnectAsync();
    }

    private void Update()
    {
        if (_isNeedToTurnOffPlayfield)
        {
            ChangeFromPlayfield();
            _isNeedToTurnOffPlayfield = false;
        }

        if (_isPlayfieldState)
        {
            ChangeToPlayfield();
            return;
        }
        
        if (_axeMessageReceived)
        {
            _manualConfigurationSwitch.TurnOffManualGamePanel();
            _axeMessageReceived = false;
            SwitchState();
        }
        if (_gottenScoreMessageReceived)
        {
            _gottenScoreMessageReceived = false;
            if(axeAttempts.AttemptsIsLeft())
            {
                scoreCounter.AddScore(_gottenScoreWebsocket.allScore,_gottenScoreWebsocket.newPoints);
            }    
        }

        if (_userMessageReceived)
        {            
            _manualConfigurationSwitch.TurnOffManualGamePanel();

            scoreCounter.ResetScore();
            axeAttempts.ResetAxesNumber();
            _userMessageReceived = false;                
            userController.SetupUserAvatars(_userWebsocket.userName,_userWebsocket.imageInBytes);
            if(_gameInfoWebsocket.gameName==GameTypeChanger.GameTypes.ShadowAxe.ToString())
            {
                trowingAxeDetection.ResetShadowAxeTargetSize();
            }
            if (_userWebsocket.cmdType == "bestUser")
            {
                panelChanger.ChangeToScorePanel();
                scoreCounter.SaveTotalScore(_userWebsocket.score);

            }
            else
            {
                
                panelChanger.ChangeToUserPanel();
                scoreCounter.SaveCurScore(_userWebsocket.score);
             
                roundsController.UseAxe(_userWebsocket.roundNumber);
            }
            

        }
        if (_onConnected)
        {
            _onConnected = false;

            ChangeToIdle();
            _manualConfigurationSwitch.enabled = true;
        }        
        if (_onClosed)
        {
            _onClosed = false;

            panelChanger.ChangeToEnterPanel();
            _manualConfigurationSwitch.enabled = false;

        }

        if (_gameInfoMessageReceived)
        {
            _gameInfoMessageReceived = false;
            axeAttempts.SetNumberAttempts(_gameInfoWebsocket.throwsCount);
            gameTypeChanger.SetupGameType(_gameInfoWebsocket.gameName);

            roundsController.SetNumberAttempts(_gameInfoWebsocket.roundsCount);
            if (_gameInfoWebsocket.gameName != GameTypeChanger.GameTypes.ShadowAxe.ToString())
            {
                defaultTarget.SetActive(true);
                shadowAxeTarget.SetActive(false);
            }
            else
            {
                defaultTarget.SetActive(false);
                shadowAxeTarget.SetActive(true);
            }
            

        }
    }

    private void SwitchState()
    {
        Debug.Log(JsonUtility.ToJson(_axeWebsocket));
        switch (_axeWebsocket.state)
        {
            case "IDLE":
                ChangeToIdle();
                break;
            case "WAITING":
                ChangeToWaiting();
                break;
            case "ARMED":
                ChangeToArmed();
                break;
            case "DETECTED":
                ChangeToDetected();
                break;
            case "SAVING":
                ChangeToSaving();
                break;
            case "COMPLETE":
                ChangeToComplete();
                break;
        }
    }

    private void ChangeToPlayfield()
    {
        Debug.Log("Changing to playfield");
        panelChanger.ChangeToPlayField();
        _isPlayfieldState = true;
    }
    
    private void ChangeFromPlayfield()
    {
        Debug.Log("Changing from playfield");
        panelChanger.ChangeFromPlayField();
        ChangeToIdle();
        _isPlayfieldState = false;
    }

    private void ChangeToIdle()
    {
        Debug.Log("Changing to idle");
        panelChanger.ChangeToStartPanel();
        textChanger.ChangeText("IDLE");
        scoreCounter.ResetScore();
        axeAttempts.ResetAxesNumber();
        trowingAxeDetection.Clear();
    }

    private void ChangeToWaiting()
    {
        Debug.Log("Changing to waiting");
        panelChanger.ChangeToGamePanel();

        textChanger.ChangeText("WAITING");
    }

    private void ChangeToArmed()
    {
        Debug.Log("Changing to armed");
        panelChanger.ChangeToGamePanel();

        textChanger.ChangeText("ARMED");

        trowingAxeDetection.Clear();
        scoreCounter.ResetScore();
    }

    private void ChangeToDetected()
    {
        Debug.Log("Changing to detected");
        panelChanger.ChangeToGamePanel();
        textChanger.ChangeText("DETECTED");
       
    }

    private void ChangeToSaving()
    {
        Debug.Log("Changing to saving");
        panelChanger.ChangeToGamePanel();

        textChanger.ChangeText("SAVING");
    }

    private void ChangeToComplete()
    {
        Debug.Log("Changing to complete");
        panelChanger.ChangeToGamePanel();

        textChanger.ChangeText("COMPLETE");
        if(_gameInfoWebsocket.gameName!=GameTypeChanger.GameTypes.ShadowAxe.ToString())
        {
            trowingAxeDetection.PlaceAxeImpact(_axeWebsocket.markerX, _axeWebsocket.markerY);
        }
        else
        {
            Debug.Log("__ "+axeAttempts.GetCurAttempt());
            trowingAxeDetection.PlaceShadowAxeImpact(_axeWebsocket.markerX, _axeWebsocket.markerY,scoreCounter.GetNumberOfHits());

        }
        
        axeAttempts.UseAxe(_axeWebsocket.markerX < 0 || _axeWebsocket.markerY < 0);
    }

    private void OnApplicationQuit()
    {
        _webSocket?.Close();
    }
}