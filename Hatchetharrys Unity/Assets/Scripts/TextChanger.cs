using TMPro;
using UnityEngine;

public class TextChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textToChange;

    public void ChangeText(string newString)
    { 
        textToChange.text = newString;
    }
}
