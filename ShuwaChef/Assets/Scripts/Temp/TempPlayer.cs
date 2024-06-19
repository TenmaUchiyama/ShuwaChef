using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TempPlayer : MonoBehaviour
{


    [SerializeField] private Transform CookingToolHoldPoint; 
    [SerializeField] private Transform IngredientToolHoldPoint;

    [SerializeField] private GameObject testObject;

    CharacterController characterController;
    private Vector3 playerVelocity; 

    public float speed = 5f; 

    private bool isWalking = false;

    void Start()
    {
        this.characterController = GetComponent<CharacterController>();

        GameInput.Instance.OnTestKnifeDetected += OnTestKnifeDetected;
    }

    private void OnTestKnifeDetected(object sender, EventArgs e)
    {
        Instantiate(testObject,CookingToolHoldPoint.transform);
    }

    private void Update() {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Debug.Log(inputVector);

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = moveDir != Vector3.zero;
        
        characterController.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime);

    }


    public bool GetIsWalking() 
    {
        return isWalking;
    }
 


   
}
