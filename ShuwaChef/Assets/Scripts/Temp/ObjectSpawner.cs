using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static DetectedObjectUI;



[Serializable]
public class ShuwaDetectorData
{
    public List<int> top_indices ;
    public List<string> top_labels;
    public List<float> dists ;
}




public class ObjectSpawner : MonoBehaviour
{




    [SerializeField] private List<ToolObject> toolObjectList;
    [SerializeField] private List<KitchenObject> kitchenObjectList;
    [SerializeField] private GameObject recordingLabel;

 

    private SocketCommunicator socketCommunicator;
    [SerializeField] private DetectedObjectUI detectedObjectUI;
    [SerializeField] private Sprite idleSprite;
    

    public event EventHandler<OnSpawnToolObjectArg> OnSpawnToolObject;
    
    public class OnSpawnToolObjectArg : EventArgs {
        public ToolObject toolObject;
    }
    public event EventHandler<OnSpawnKitchenObjectArg> OnSpawnKitchenObject;
    
    public class OnSpawnKitchenObjectArg : EventArgs {
        public KitchenObject kitchenObject;
    }

    public event EventHandler OnIdleDetected;



    private void Start() {



         
        Application.runInBackground = true; 

        socketCommunicator = GetComponent<SocketCommunicator>();
     
        socketCommunicator.OnSocketMessageReceive += SocketCommunicator_OnSocketMessageReceived;
        GameInput.Instance.OnRecordAction += GameInput_OnRecordAction;
    }
   




    private void GameInput_OnRecordAction(object sender, EventArgs e)
    {
        if(!KitchenGameManager.Instance.IsGamePlaying())
        {
            return;
        }
       socketCommunicator.RecordRequest();
    }


    


    private void SocketCommunicator_OnSocketMessageReceived(object sender, SocketCommunicator.OnSocketMessageReceiveArg e)
    {


        Debug.Log($"<color=yellow>{e.message}</color>");

        
        if(e.message == "record")
        {
          Debug.Log("Recording");
            recordingLabel.SetActive(true);
          return;
        }
        recordingLabel.SetActive(false);

    
        




        ShuwaDetectorData shuwaDetectorData = JsonUtility.FromJson<ShuwaDetectorData>(e.message);

        string spawnObject = shuwaDetectorData.top_labels[0];



        List<DetectedObject> detectedObjects = new List<DetectedObject>();
          for(int i = 0 ; i< 3 ; i++)
         {

           
            
            ToolObject toolObject = toolObjectList.Find(x => x.GetToolObjectSO().objectName == shuwaDetectorData.top_labels[i]);
            KitchenObject kitchenObject = kitchenObjectList.Find(x => x.GetKitchenObjectSO().objectName == shuwaDetectorData.top_labels[i]);
    
            if(toolObject)
            {
                DetectedObject detectedObject = new DetectedObject(){name = toolObject.GetToolObjectSO().objectName, sprite = toolObject.GetToolObjectSO().sprite, probability = shuwaDetectorData.dists[i]};
                detectedObjects.Add(detectedObject);
            }else if(kitchenObject){
                DetectedObject detectedObject = new DetectedObject(){name = kitchenObject.GetKitchenObjectSO().objectName, sprite = kitchenObject.GetKitchenObjectSO().sprite, probability = shuwaDetectorData.dists[i]};
                detectedObjects.Add(detectedObject);
            }else{
                DetectedObject detectedObject = new DetectedObject(){name = "Idle", sprite = idleSprite, probability = shuwaDetectorData.dists[i]};
                detectedObjects.Add(detectedObject);
            }
            
            
         }

        detectedObjectUI.SetDetectedObject(detectedObjects);

        
        HandleObjectSpawn(spawnObject);
    }





    

  
    private void HandleObjectSpawn(string spawnObject)
    {

       
     
        ToolObject toolObject = toolObjectList.Find(x => x.GetToolObjectSO().objectName == spawnObject);
        KitchenObject kitchenObject = kitchenObjectList.Find(x => x.GetKitchenObjectSO().objectName == spawnObject);


        if(toolObject )
        {
            OnSpawnToolObject?.Invoke(this, new OnSpawnToolObjectArg {
                toolObject = toolObject
            });

            
        }


        if(kitchenObject)
        {
            OnSpawnKitchenObject?.Invoke(this, new OnSpawnKitchenObjectArg {
                kitchenObject = kitchenObject
            });
        }


        if(!toolObject && !kitchenObject)
        {
            OnIdleDetected?.Invoke(this, EventArgs.Empty);
        }



        


    }



    // private void Update() {
    //     TestSpanw();


    // }



    private void TestSpanw() 
    {
        

        if(Input.GetKeyDown(KeyCode.Alpha1)) 
         {
            OnSpawnToolObject?.Invoke(this, new OnSpawnToolObjectArg {
                toolObject = toolObjectList.Find(x => x.GetToolObjectSO().objectName == "Pan")
            });
          

         }

         if(Input.GetKeyDown(KeyCode.Alpha2)) 
         {
            OnSpawnToolObject?.Invoke(this, new OnSpawnToolObjectArg {
                toolObject = toolObjectList.Find(x => x.GetToolObjectSO().objectName == "Knife")
            });
          

         }
         if(Input.GetKeyDown(KeyCode.Alpha3)) 
         {
                
                    OnSpawnKitchenObject?.Invoke(this, new OnSpawnKitchenObjectArg {
                        kitchenObject = kitchenObjectList.Find(x => x.GetKitchenObjectSO().objectName == "Tomato")
                    });
    
         }
         if(Input.GetKeyDown(KeyCode.Alpha4)) 
         {
            
                    OnSpawnKitchenObject?.Invoke(this, new OnSpawnKitchenObjectArg {
                        kitchenObject = kitchenObjectList.Find(x => x.GetKitchenObjectSO().objectName == "Cabbage")
                    });
         }
         if(Input.GetKeyDown(KeyCode.Alpha5)) 
         {

                OnSpawnKitchenObject?.Invoke(this, new OnSpawnKitchenObjectArg {
                    kitchenObject = kitchenObjectList.Find(x => x.GetKitchenObjectSO().objectName == "Cheese Block")
                });
         }
         if(Input.GetKeyDown(KeyCode.Alpha6)) 
         {

                OnSpawnKitchenObject?.Invoke(this, new OnSpawnKitchenObjectArg {
                    kitchenObject = kitchenObjectList.Find(x => x.GetKitchenObjectSO().objectName == "Meat Patty Uncooked")
                });
         }
         if(Input.GetKeyDown(KeyCode.Alpha7)) 
         {

                OnSpawnKitchenObject?.Invoke(this, new OnSpawnKitchenObjectArg {
                    kitchenObject = kitchenObjectList.Find(x => x.GetKitchenObjectSO().objectName == "Bread")
                });
         }

         if(Input.GetKeyDown(KeyCode.Alpha8)) 
         {

                OnSpawnKitchenObject?.Invoke(this, new OnSpawnKitchenObjectArg {
                    kitchenObject = kitchenObjectList.Find(x => x.GetKitchenObjectSO().objectName == "Plate")
                });
         }
    }

}
