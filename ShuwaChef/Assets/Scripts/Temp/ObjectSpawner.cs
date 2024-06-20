using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField] private List<ToolObject> toolObjectList;
    [SerializeField] private List<KitchenObject> kitchenObjectList;



    public event EventHandler<OnSpawnToolObjectArg> OnSpawnToolObject;
    
    public class OnSpawnToolObjectArg : EventArgs {
        public ToolObject toolObject;
    }
    public event EventHandler<OnSpawnKitchenObjectArg> OnSpawnKitchenObject;
    
    public class OnSpawnKitchenObjectArg : EventArgs {
        public KitchenObject kitchenObject;
    }


    private void Update() {
        HandleObjectSpawn();
    }

    private void HandleObjectSpawn()
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
