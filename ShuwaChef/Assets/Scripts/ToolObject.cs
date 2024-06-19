using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolObject : MonoBehaviour
{
    [SerializeField] private ToolObjectSO toolObjectSO;

     private IKitchenObjectParent kitchenObjectParent;

     public ToolObjectSO GetToolObjectSO() {
        return toolObjectSO;
    }


    public static ToolObject SpawnKitchenObject(ToolObjectSO toolObjectSO, IKitchenObjectParent toolObjectParent) {
        Transform toolObjectTransform = Instantiate(toolObjectSO.prefab);

        ToolObject toolObject = toolObjectTransform.GetComponent<ToolObject>();
        
        // toolObject.SetKitchenObjectParent(kitchenObjectParent);

        return toolObject;
    }

}
