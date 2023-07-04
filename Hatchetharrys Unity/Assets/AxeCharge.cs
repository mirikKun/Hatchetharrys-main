using UnityEngine;
using UnityEngine.UI;

public class AxeCharge : MonoBehaviour
{
    [SerializeField] private Sprite grayAxe;
    [SerializeField] private Sprite redAxe;
    [SerializeField] private Image axeIcon;
    [SerializeField] private Image missIcon;

    public void ResetAxeCharge()
    {
        axeIcon.sprite=grayAxe;
        missIcon.enabled = false;
    }

    public void UseAxe(bool miss)
    {
        if (miss)
        {
            missIcon.enabled = true;
        }
        else
        {
            axeIcon.sprite=redAxe;

        }
        
    }

}
