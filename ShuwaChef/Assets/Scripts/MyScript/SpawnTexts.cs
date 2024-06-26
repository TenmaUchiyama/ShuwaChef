using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnTexts : MonoBehaviour
{

     public static SpawnTexts Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI spawnTexts;

    private Coroutine surviveCoroutine;
    private void Awake() {
        Instance = this;
    }
   public void SetDetectedObject(string spawnTexts)
   {
        if(surviveCoroutine != null)
          {
               StopCoroutine(surviveCoroutine);
          }
        this.gameObject.SetActive(true);
        this.spawnTexts.text = spawnTexts;
        surviveCoroutine = StartCoroutine(SurviveTime());
   }




   private IEnumerator SurviveTime() 
   {
         yield return new WaitForSeconds(1f);
         this.gameObject.SetActive(false);
   }


   

}
