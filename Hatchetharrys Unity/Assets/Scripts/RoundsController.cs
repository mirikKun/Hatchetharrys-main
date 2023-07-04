using UnityEngine;

public class RoundsController : MonoBehaviour
{
   [SerializeField] private Transform roundChargesParent;
   [SerializeField] private RoundCharge roundChargePrefab;
   private int _maxNumberOfRounds = 3;
   private int _curRoundsLeft ;

   private int _lastRound;
   
   private RoundCharge[] _roundCharges;
   
   private void Start()
   {
      ResetRoundsNumber();
   }

   public void SetNumberAttempts(int newNumberOfRounds)
   {
      foreach (Transform child in roundChargesParent) 
      {
             Destroy(child.gameObject);
      }
      ResetRoundsNumber();
      _maxNumberOfRounds = newNumberOfRounds;
      _curRoundsLeft = newNumberOfRounds;
      _roundCharges = new RoundCharge[newNumberOfRounds];
      for (int i = 0; i < newNumberOfRounds; i++)
      {
         _roundCharges[i] = Instantiate(roundChargePrefab, roundChargesParent);
      }
      
   }

   public void ResetRoundsNumber()
   {
      _curRoundsLeft = _maxNumberOfRounds;
      if (_roundCharges == null)
      {
         return;
      }
      foreach (var axeCharge in _roundCharges)
      {
         axeCharge.ResetRoundCharge();
      }
   }

   public void UseAxe(int newRound)
   {
      if(_curRoundsLeft<1||_lastRound==newRound)
         return;
      _lastRound = newRound;
      _roundCharges[_curRoundsLeft-1].UseRound();
      _curRoundsLeft--;
   }
}
