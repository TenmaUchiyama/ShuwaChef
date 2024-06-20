using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour, IKitchenObjectParent,IToolObjectParent
{
   
    [SerializeField] private float speed = 7f;
    [SerializeField] ObjectSpawner objectSpawner;


    public static Player Instance { get; private set; }
    CharacterController characterController;




    



    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }


    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    [SerializeField] private Transform toolObjectHoldPoint;

    


    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;

    private KitchenObject kitchenObject;
    private ToolObject toolObject; 



    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start() {
        this.characterController = GetComponent<CharacterController>();
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
        objectSpawner.OnSpawnToolObject += ObjectSpawner_OnSpawnToolObject;
        objectSpawner.OnSpawnKitchenObject += ObjectSpawner_OnKitchenObjectSpawned;
    }

    private void ObjectSpawner_OnKitchenObjectSpawned(object sender, ObjectSpawner.OnSpawnKitchenObjectArg e)
    {
       if(this.HasKitchenObject())
        {
            this.kitchenObject.DestroySelf();
        }
        KitchenObject.SpawnKitchenObject(e.kitchenObject.GetKitchenObjectSO(), this);
    }

    private void ObjectSpawner_OnSpawnToolObject(object sender, ObjectSpawner.OnSpawnToolObjectArg e)
    {
      
 
        
        if(this.HasToolObject())
        {
            this.toolObject.DestroySelf();
        }
        ToolObject.SpawnToolObject(e.toolObject.GetToolObjectSO(), this);
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
        // if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        
        // if (!KitchenGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
 
    }

 
    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        Transform mainCameraTransform = Camera.main.transform;
        Vector3 rayDirection = mainCameraTransform.forward;
        Debug.DrawRay(mainCameraTransform.position, rayDirection * interactDistance, Color.red);

        if (Physics.Raycast(mainCameraTransform.position, rayDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                // Has ClearCounter
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);

            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement() {
            Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
         

            Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
            isWalking = moveDir != Vector3.zero;
            
            characterController.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime);

    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }


    public Transform GetToolObjectFollowTransform() {
        return toolObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public void SetToolObject(ToolObject toolObject)
    {
        this.toolObject = toolObject;

        if ( this.toolObject != null) {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public ToolObject GetToolObject()
    {
        return this.toolObject;
    }

    public void ClearToolObject()
    {
        this.toolObject = null;
    }

    public bool HasToolObject()
    {
        return this.toolObject != null;
    }
}
