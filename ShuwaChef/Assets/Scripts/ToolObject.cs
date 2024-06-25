using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolObject : MonoBehaviour
{
    [SerializeField] private ToolObjectSO toolObjectSO;
    [SerializeField] private string TriggerAnimationName = "Cut";

    private Animator animator;

     private IToolObjectParent toolObjectParent;

     public ToolObjectSO GetToolObjectSO() {
        return toolObjectSO;
    }


    private void Start() {
        animator = GetComponent<Animator>();
    
    }
 public void SetToolObjectParent(IToolObjectParent toolObjectParent) {
        if (this.toolObjectParent != null) {
            this.toolObjectParent.ClearToolObject();
        }

        this.toolObjectParent = toolObjectParent;
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


    public void PlayAnimation() 
    {
        animator.SetTrigger(TriggerAnimationName);
    }

}
