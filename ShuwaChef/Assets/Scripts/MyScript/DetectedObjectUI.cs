using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class DetectedObjectUI : MonoBehaviour
{
    [SerializeField] private Image image1;
   [SerializeField] private TextMeshProUGUI probability1; 

    [SerializeField] private Image image2;
   [SerializeField] private TextMeshProUGUI probability2; 

    [SerializeField] private Image image3;
   [SerializeField] private TextMeshProUGUI probability3; 
    private Coroutine surviveCoroutine;

   

   
   public class DetectedObject 
   {
         public string name;
         public Sprite sprite;
         public float probability;
   }





   
   public void SetDetectedObject(List<DetectedObject> detectedObjects)
   {



       if(surviveCoroutine != null)
         {
              StopCoroutine(surviveCoroutine);
         }
       this.gameObject.SetActive(true);

        image1.sprite = detectedObjects[0].sprite;
        var name = detectedObjects[0].name; 
        
        probability1.text = $"{detectedObjects[0].name}: {detectedObjects[0].probability.ToString("0.00")}";

         image2.sprite = detectedObjects[1].sprite;
        probability2.text = $"{detectedObjects[1].name}: {detectedObjects[1].probability.ToString("0.00")}";

         image3.sprite = detectedObjects[2].sprite;
        probability3.text = $"{detectedObjects[2].name}: {detectedObjects[2].probability.ToString("0.00")}";



        surviveCoroutine = StartCoroutine(SurviveTime());


   }


   private string BeefConvert(string objName)
   {
      return objName == "Meat Patty Uncooked" ? "Beef" : objName;
   }

   private IEnumerator SurviveTime() 
   {
         yield return new WaitForSeconds(3f);
         this.gameObject.SetActive(false);
   }


   






}
