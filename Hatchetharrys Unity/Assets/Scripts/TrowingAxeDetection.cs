using TMPro;
using UnityEngine;

public class TrowingAxeDetection : MonoBehaviour
{
    [SerializeField] private RectTransform targetTransform;    
    [SerializeField] private Transform shadowAxeTargetTransform;

    private int _openCVTargetXSize = 300;
    private int _openCVTargetYSize = 300;
    [Range(0, 1)] [SerializeField] private float firstСircleFactor = 0.18f;
    [Range(0, 1)] [SerializeField] private float secondСircleFactor = 0.40f;
    [Range(0, 1)] [SerializeField] private float thirdСircleFactor = 0.68f;

    [SerializeField] private RectTransform hitImpact;

    [SerializeField] private TextMeshProUGUI axisHit;
    
    
    private float _unityTargetXSize;
    private float _unityTargetYSize;

    private void Awake()
    {
        var sizeDelta = targetTransform.sizeDelta;
        _unityTargetXSize = sizeDelta.x;
        _unityTargetYSize = sizeDelta.y;
        
    }

    private void Update()
    {

       
        // var distance = Vector2.Distance(hitImpact.anchoredPosition, Vector2.zero);
        //
        // Debug.Log(distance / (_unityTargetXSize / 2));
        
    }

    public int PlaceAxeImpact(int xPos, int yPos)
    {
        hitImpact.gameObject.SetActive(true);
        int curPoints;
        var newPos = new Vector2(
            (xPos - _openCVTargetXSize / 2) * _unityTargetXSize / _openCVTargetXSize,
            -(yPos - _openCVTargetYSize / 2) * _unityTargetYSize / _openCVTargetYSize);
        hitImpact.anchoredPosition = newPos;
        axisHit.text = newPos.ToString();
        var distance = Vector2.Distance(newPos, Vector2.zero);
        if (distance / (_unityTargetXSize / 2) < firstСircleFactor)
        {
            curPoints = 5;
        }
        else if(distance / (_unityTargetXSize / 2) < secondСircleFactor)
        {
            curPoints = 3;
        }
        else if(distance / (_unityTargetXSize / 2) < thirdСircleFactor)
        {
            curPoints = 1;
        }
        else
        {
            curPoints = 0;
        }

        return curPoints;
    }
    
    private  float startShadowAxeCircleFactor = 0.8f;

    public  int PlaceShadowAxeImpact(int xPos, int yPos, int  numberOfHits)
    {
        hitImpact.gameObject.SetActive(true);

        int curPoints;
        var newPos = new Vector2(
            (xPos - _openCVTargetXSize / 2) * _unityTargetXSize / _openCVTargetXSize,
            -(yPos - _openCVTargetYSize / 2) * _unityTargetYSize / _openCVTargetYSize);
        
        hitImpact.anchoredPosition = newPos;
        axisHit.text = newPos.ToString();
        var distance = Vector2.Distance(newPos, Vector2.zero);
        if (distance / (_unityTargetXSize / 2) < startShadowAxeCircleFactor-0.07*numberOfHits)
        {

            curPoints = numberOfHits+1;
        }
        else
        {
            curPoints = 0;
        }

        shadowAxeTargetTransform.localScale = (startShadowAxeCircleFactor - 0.07f * numberOfHits) * Vector3.one;

        return curPoints;
    }

    public void ResetShadowAxeTargetSize()
    {
        shadowAxeTargetTransform.localScale = startShadowAxeCircleFactor * Vector3.one;

    }
    
    public void Clear()
    {
        hitImpact.gameObject.SetActive(false);
    }   

}
