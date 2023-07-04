using UnityEngine;

public class PanelChanger : MonoBehaviour
{
    [SerializeField] private GameObject ipAddress;
    [SerializeField] private GameObject startPanel;

    [SerializeField] private GameObject userPanel;

    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject scorePanel;

    [SerializeField] private GameObject[] gamePanelItemsToHide;

    private GameObject _lastPanel;

    private GameObject _panelBeforeManualChanging;
    private void Start()
    {
        _lastPanel = ipAddress;
    }

    public void TurnOffLastPanel(GameObject newLastPanel)
    {
        if(newLastPanel==_lastPanel)
            return;
        _lastPanel.SetActive(false);
        _lastPanel = newLastPanel;
    }

    public void ChangeToStartPanel()
    {
        TurnOffLastPanel(startPanel);
        startPanel.SetActive(true);

    }

    public void ChangeToEnterPanel()
    {        

        TurnOffLastPanel(ipAddress);
        ipAddress.SetActive(true);

    }

    public void ChangeToGamePanel()
    {
        TurnOffLastPanel(gamePanel);
        gamePanel.SetActive(true);
    }
    
    public void ChangeToPlayField()
    {
        TurnOffLastPanel(gamePanel);
        gamePanel.SetActive(true);
        foreach (var item in gamePanelItemsToHide)
        {
            item.SetActive(false);
        }
    }
    
    public void ChangeFromPlayField()
    {
        foreach (var item in gamePanelItemsToHide)
        {
            item.SetActive(true);
        }
    }

    public void ChangeToScorePanel()
    {
        TurnOffLastPanel(scorePanel);
        scorePanel.SetActive(true);

    }    
    public void ChangeToUserPanel()
    {
        TurnOffLastPanel(userPanel);
        userPanel.SetActive(true);
    }

    public void ManualChangeToGamePanel()
    {
        _panelBeforeManualChanging = _lastPanel;
        _lastPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    public void ManualChangeBackFromGamePanel()
    {
        gamePanel.SetActive(false);
        _panelBeforeManualChanging.SetActive(true);

    }
    
}