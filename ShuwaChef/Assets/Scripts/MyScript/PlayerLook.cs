using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{


    public static PlayerLook Instance; 
    public Transform cameraTransform; 
    private float xRotation = 0f; 


    public float xSensitivity = 30f;
    public float ySensitivity = 30f;


    private void Awake() {
        Instance = this;
    }
    public void Update()
    {


        Vector2 mouseDelta = GameInput.Instance.GetMouseMoveDelta();
  
        float mouseX = mouseDelta.x; 
        float mouseY = mouseDelta.y; 

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity; 
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); 

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX * Time.deltaTime * xSensitivity);
    }


    public  float[] GetSensitivity()
    {
        return new float[] {this.xSensitivity, this.ySensitivity};
    } 

    public void SetSensitivity(float xSensitivity, float ySensitivity)
    {
        this.xSensitivity = xSensitivity;
        this.ySensitivity = ySensitivity;
    }

}
