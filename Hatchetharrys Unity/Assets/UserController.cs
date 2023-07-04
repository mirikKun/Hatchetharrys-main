using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] userNames;
    [SerializeField] private Image[] userAvatars;

    public void SetupUserAvatars(string newUserName,string newUserAvatars)
    {
        Sprite newAvatar = GetSpriteFromString(newUserAvatars);
        foreach (var userName in userNames)
        {
            userName.text = newUserName;
        }   
        foreach (var userAvatar in userAvatars)
        {
            userAvatar.sprite = newAvatar;
        }
    }

    private Sprite GetSpriteFromString(string stringToSprite)
    {
        byte[]  imageBytes = Convert.FromBase64String(stringToSprite);
        Texture2D tex = new Texture2D(500, 500);
        tex.LoadImage( imageBytes );
        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        return sprite;
    }
}
