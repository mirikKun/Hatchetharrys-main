using UnityEngine;
using UnityEngine.UI;

public class GameTypeChanger : MonoBehaviour
{
    [SerializeField] private Image curGameIcon;
    [SerializeField] private Image curGameNameText;
    [SerializeField] private GameInfo[] games;

    [SerializeField] private GameObject[] objectsToHideInInfinite;
    
    public enum GameTypes
    {
        InfinateAxe,
        KickAxe,
        ShadowAxe
    }
    [System.Serializable]

    public struct GameInfo
    {
        public string gameName;
        public Sprite gameNameImage;
        public Sprite gameIcon;
    }

    public void SetupGameType(string gameName)
    {
        foreach (var game in games)
        {                
            Debug.Log(game.gameName+"  "+gameName);
            if (game.gameName == gameName)
            {
                curGameIcon.sprite = game.gameIcon;
                curGameNameText.sprite = game.gameNameImage;
            }
        }
        if (gameName == GameTypes.InfinateAxe.ToString())
        {
            foreach (var objectToHideInInfinite in objectsToHideInInfinite)
            {
                objectToHideInInfinite.SetActive(false);
            }
        }
        else if (gameName == GameTypes.KickAxe.ToString())
        {
            foreach (var objectToHideInInfinite in objectsToHideInInfinite)
            {
                objectToHideInInfinite.SetActive(true);
            }
        }        
        else if (gameName == GameTypes.ShadowAxe.ToString())
        {
            foreach (var objectToHideInInfinite in objectsToHideInInfinite)
            {
                objectToHideInInfinite.SetActive(true);
            }
        }
    }
}
