using UnityEngine;

public class AxeAttempts : MonoBehaviour
{
   [SerializeField] private Transform axeChargesParent;
   [SerializeField] private AxeCharge axeChargePrefab;
   private int maxNumberOfAttempts = 5;
   private int _curAttemptsLeft ;

   
   private AxeCharge[] _axeCharges;
   
   private void Start()
   {
      ResetAxesNumber();
   }

   public void SetNumberAttempts(int newNumberOfAttempts)
   {
      if(newNumberOfAttempts==0)
      {
         
      }
      foreach (Transform child in axeChargesParent) 
      {
             Destroy(child.gameObject);
      }
      ResetAxesNumber();
      maxNumberOfAttempts = newNumberOfAttempts;
      _curAttemptsLeft = newNumberOfAttempts;
      _axeCharges = new AxeCharge[newNumberOfAttempts];
      for (int i = 0; i < newNumberOfAttempts; i++)
      {
         _axeCharges[i] = Instantiate(axeChargePrefab, axeChargesParent);
      }
      
   }

   public int GetCurAttempt()
   {
      return maxNumberOfAttempts - _curAttemptsLeft;
   }
   public void ResetAxesNumber()
   {
      _curAttemptsLeft = maxNumberOfAttempts;
      if (_axeCharges == null)
      {
         return;
      }
      foreach (var axeCharge in _axeCharges)
      {
         axeCharge.ResetAxeCharge();
      }
   }

   public bool AttemptsIsLeft()
   {
      return _curAttemptsLeft > 0||maxNumberOfAttempts==0;
   }
   public void UseAxe(bool miss)
   {
      if(_curAttemptsLeft<1||maxNumberOfAttempts==0)
         return;
      _axeCharges[_curAttemptsLeft-1].UseAxe(miss);
      _curAttemptsLeft--;
   }
}
