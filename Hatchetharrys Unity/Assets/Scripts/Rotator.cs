using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    private bool _portraitOnLandscape=true;
  

    public void Rotate()
    {
        if (Screen.width>Screen.height)
        {
            if (_portraitOnLandscape)
            {
                rectTransform.eulerAngles = new Vector3(0, 0,0);
                rectTransform.localScale=Vector3.one;
                _portraitOnLandscape = false;
            }
            else
            {
                rectTransform.eulerAngles = new Vector3(0, 0,-90);
                rectTransform.localScale = new Vector3(1.7f, 1.7f,1);
                _portraitOnLandscape = true;

            }
            
        }
    }
}
