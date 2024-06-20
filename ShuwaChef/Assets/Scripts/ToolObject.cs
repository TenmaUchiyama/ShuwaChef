using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolObject : MonoBehaviour
{
    [SerializeField] private ToolObjectSO toolObjectSO;

     private IToolObjectParent toolObjectParent;

     public ToolObjectSO GetToolObjectSO() {
        return toolObjectSO;
    }
 public void SetToolObjectParent(IToolObjectParent toolObjectParent) {
        if (this.toolObjectParent != null) {
            this.toolObjectParent.ClearToolObject();
        }

        this.toolObjectParent = toolObjectParent;
          Debug.Log(this.toolObjectParent.GetToolObject());
        if (toolObjectParent.HasToolObject()) {
            Debug.LogError("ToolObjectParent already has a ToolObject!");
        }

        toolObjectParent.SetToolObject(this);

        transform.parent = toolObjectParent.GetToolObjectFollowTransform();
        transform.forward = toolObjectParent.GetToolObjectFollowTransform().forward;
        transform.localPosition = Vector3.zero;

    }


     public void DestroySelf() {
        toolObjectParent.ClearToolObject();

        Destroy(gameObject);
    }

    public static ToolObject SpawnToolObject(ToolObjectSO toolObjectSO, IToolObjectParent toolObjectParent) {
        Transform toolObjectTransform = Instantiate(toolObjectSO.prefab);

        ToolObject toolObject = toolObjectTransform.GetComponent<ToolObject>();
        
        toolObject.SetToolObjectParent(toolObjectParent);

        return toolObject;
    }

}
