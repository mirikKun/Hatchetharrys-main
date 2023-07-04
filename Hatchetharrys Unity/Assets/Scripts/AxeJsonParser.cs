using System;
using UnityEngine;

public class AxeJsonParser : MonoBehaviour
{

    [SerializeField] private FromJsonAxeWebsocket axeWebsocket = new FromJsonAxeWebsocket();
    [SerializeField] private FromJsonUserWebsocket userWebsocket = new FromJsonUserWebsocket();
    [SerializeField] private FromJsonGameInfoWebsocket gameInfoWebsocket = new FromJsonGameInfoWebsocket();

    [SerializeField] private FromJsonGottenScoreWebsocket gottenScoreWebsocket = new FromJsonGottenScoreWebsocket();

    public FromJsonAxeWebsocket ParseRaspberryPieString(string incomingString)
    {

        axeWebsocket = new FromJsonAxeWebsocket();
        try
        {
            axeWebsocket=JsonUtility.FromJson<FromJsonAxeWebsocket>(incomingString);
            if(axeWebsocket.IsDefault())
            {
                axeWebsocket = new FromJsonAxeWebsocket();
                Debug.Log("wrongAxe");

            }
            else
            {
                Debug.Log("rightAxe");

            }

        }
        catch (Exception e)
        {
            Debug.Log("wrong axe");
            // ignored
        }

        return axeWebsocket;
    }
    
    public FromJsonUserWebsocket ParseUserString(string incomingString)
    {

        userWebsocket = new FromJsonUserWebsocket();
        try
        {
            userWebsocket=JsonUtility.FromJson<FromJsonUserWebsocket>(incomingString);
        }
        catch (Exception e)
        {
            Debug.Log("wrong user");
        }

        return userWebsocket;
    }    
    public FromJsonGameInfoWebsocket ParseGameInfoString(string incomingString)
    {

        gameInfoWebsocket = new FromJsonGameInfoWebsocket();
        try
        {
            gameInfoWebsocket=JsonUtility.FromJson<FromJsonGameInfoWebsocket>(incomingString);
        }
        catch (Exception e)
        {
            Debug.Log("wrong user");
        }

        return gameInfoWebsocket;
    }

    public FromJsonGottenScoreWebsocket ParseGottenScoreString(string incomingString)
    {
        gottenScoreWebsocket = new FromJsonGottenScoreWebsocket();
        try
        {
            gottenScoreWebsocket=JsonUtility.FromJson<FromJsonGottenScoreWebsocket>(incomingString);
        }
        catch (Exception e)
        {
            Debug.Log("wrong user");
        }
        return gottenScoreWebsocket;
    }

}
[System.Serializable]
    public struct FromJsonAxeWebsocket
    {
        public string cmdType;
        public string state;

        public int markerX;
        public int markerY;

        public override string ToString()
        {
            return cmdType+" | "+state;
        }

        public bool IsDefault()
        {
            return  cmdType !="state" ;
        }
    }
[System.Serializable]
public struct FromJsonUserWebsocket
{
    public string cmdType;
    public string userName;
    public int score;
    public int roundNumber;
    public string imageInBytes;

    public override string ToString()
    {
        return cmdType+" | "+userName+" | "+imageInBytes;
    }

    public bool IsDefault()
    {
        return  userName == null&& imageInBytes == null;
    }
}
[System.Serializable]
public struct FromJsonGameInfoWebsocket
{
    public string cmdType;
    public int throwsCount;
    public int roundsCount;
    public string gameName;
    public override string ToString()
    {
        return cmdType+" | "+throwsCount;
    }

    public bool IsDefault()
    {
        return  cmdType != "throwsCount";
    }
}

[System.Serializable]
public struct FromJsonGottenScoreWebsocket
{
    public string cmdType;
    public string gameType;
    public int allScore;
    public int newPoints;

    public bool IsDefault()
    {
        return gameType == null && allScore == 0;
    }
}