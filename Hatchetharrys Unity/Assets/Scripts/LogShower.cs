using UnityEngine;
using UnityEngine.UI;

public class LogShower : MonoBehaviour
{
   [SerializeField] private GameObject logs;
   [SerializeField] private Text _text;

   private bool logsOpened;
   public void OpenCloseLogs()
   {
       logs.SetActive(!logsOpened);
         logsOpened = !logsOpened;
   }

   public void AddLogs(string newLog)
   {
       _text.text += "\n"+newLog;
   }
}
