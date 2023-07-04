using UnityEngine;
using UnityEngine.UI;

public class TestStateSwitch : MonoBehaviour
{
    [SerializeField] private Text buttonText;
    [SerializeField] private GameObject[] testObjects;
    [SerializeField] private string defaultText = "state panel";
    private bool _buttonOn = true;

    public void Switch()
    {
        if (_buttonOn)
        {
            foreach (var testObject in testObjects)
            {
                testObject.SetActive(false);
            }

            buttonText.text = defaultText + " off";
            _buttonOn = false;
        }
        else
        {
            foreach (var testObject in testObjects)
            {
                testObject.SetActive(true);
            }

            buttonText.text = defaultText + " on";
            _buttonOn = true;
        }
    }
}