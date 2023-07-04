using UnityEngine;
using UnityEngine.UI;

public class RoundCharge : MonoBehaviour
{
    [SerializeField] private Sprite grayRound;
    [SerializeField] private Sprite blackRound;
    [SerializeField] private Image roundIcon;

    public void ResetRoundCharge()
    {
        roundIcon.sprite=grayRound;
    }

    public void UseRound()
    {
        roundIcon.sprite=blackRound;
    }
}
