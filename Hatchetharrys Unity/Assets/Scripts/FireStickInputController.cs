using TMPro;
using UnityEngine;

public class FireStickInputController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void Update()
    {
        textMeshProUGUI.text = Input.mousePosition.ToString();
    }
}
