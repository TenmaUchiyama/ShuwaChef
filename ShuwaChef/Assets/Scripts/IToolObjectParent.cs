using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IToolObjectParent {

    public Transform GetToolObjectFollowTransform();

    public void SetToolObject(ToolObject toolObject);

    public ToolObject GetToolObject();

    public void ClearToolObject();

    public bool HasToolObject();

}