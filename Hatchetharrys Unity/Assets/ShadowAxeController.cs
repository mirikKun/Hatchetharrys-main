using UnityEngine;

public class ShadowAxeController : MonoBehaviour
{
    private  int _openCVTargetXSize = 300;
    private  int _openCVTargetYSize = 300;
    private  float firstСircleFactor = 0.18f;
    private  float secondСircleFactor = 0.40f;
    private  float thirdСircleFactor = 0.68f;

    private  float _unityTargetXSize=600;
    private  float _unityTargetYSize=600;
    public  int CalculateScore(int xPos, int yPos)
    {
        int curPoints;
        var newPos = new Vector2(
            (xPos - _openCVTargetXSize / 2) * _unityTargetXSize / _openCVTargetXSize,
            -(yPos - _openCVTargetYSize / 2) * _unityTargetYSize / _openCVTargetYSize);
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

    [SerializeField] private Transform newTarget;
    
    public  int CalculateShadowAxeScore(int xPos, int yPos, int  numberOfHits)
    {
        int curPoints;
        var newPos = new Vector2(
            (xPos - _openCVTargetXSize / 2) * _unityTargetXSize / _openCVTargetXSize,
            -(yPos - _openCVTargetYSize / 2) * _unityTargetYSize / _openCVTargetYSize);
        var distance = Vector2.Distance(newPos, Vector2.zero);
        if (distance / (_unityTargetXSize / 2) < startShadowAxeCircleFactor-0.07*numberOfHits)
        {

            curPoints = numberOfHits+1;
        }
        else
        {
            curPoints = 0;
        }

        newTarget.localScale = (startShadowAxeCircleFactor - 0.07f * numberOfHits) * Vector3.one;

        return curPoints;
    }

}
