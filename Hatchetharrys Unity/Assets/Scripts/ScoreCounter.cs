using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextChanger curScoreTextChanger;
    [SerializeField] private TextChanger totalScoreTextChanger;
    
    private int _scoreCount;
    private int _numberOfHits=0;
    private int _newPoints;
    public int GetScore()
    {
        return _scoreCount;
    }    
    public int GetNewPoints()
    {
        return _newPoints;
    }
    public void ResetScore()
    {
        SetScore(0);
    }

    public int GetNumberOfHits()
    {
        return _numberOfHits;
    }
    public void SetScore(int newScore)
    {            
        _numberOfHits = 0;

        _scoreCount = newScore;
        curScoreTextChanger.ChangeText(_scoreCount.ToString());
    }        
    public void AddScore(int newScore,int newPoints)
    {
        if (newScore!=0)
        {
            _numberOfHits++;}
        else
        {
            _numberOfHits = 0;
        }
        _newPoints = newPoints;
        _scoreCount =newScore;
        curScoreTextChanger.ChangeText(_scoreCount.ToString());
    }    
    public void SaveTotalScore(int totalScore)
    {

        totalScoreTextChanger.ChangeText(totalScore.ToString());
    }

    public void SaveCurScore(int curScore)
    {
                curScoreTextChanger.ChangeText(curScore.ToString());

    }
}
