using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform cameraTransform; 
    private float xRotation = 0f; 


    public float xSensitivity = 30f;
    public float ySensitivity = 30f;


    public void Update()
    {


        Vector2 mouseDelta = GameInput.Instance.GetMouseMoveDelta();
        Debug.Log(mouseDelta);
        float mouseX = mouseDelta.x; 
        float mouseY = mouseDelta.y; 

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity; 
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); 

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX * Time.deltaTime * xSensitivity);
    }
}
