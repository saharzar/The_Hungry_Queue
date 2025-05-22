using UnityEngine;
using System;

public class Player : MonoBehaviour , IKitchenObjectParent

{
  
    public static Player Instance { get; private set; }
    //we did it static so it can only belongs to the the class Player itself
    
   
        

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }

    [SerializeField] public float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;




    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter SelectedCounter;
    private KitchenObject kitchenObject;


    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        // this is to listen to the interaction that the player will do 
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e)
    {
        if (SelectedCounter != null)
        {
            SelectedCounter.InteractAlternate(this);
        }

    }


    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(SelectedCounter != null)
        {
            SelectedCounter.Interact(this);
        }

    }


    private void Update()
    {
        HandleMovement();
        HandleInteractions();   
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleInteractions()    
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        //we use this so when player is not moving its still detedcting the counter
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        // this code for the clear counter thing
        float interactDistance = 2f;
        //we added the layer thing so the player whenever he sees an object with that specific layer it gives an interaction
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance,countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) //to detect the counter
            {
                // Has ClearCounter
                if (baseCounter != SelectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                   
                }


            } else
            {
                SetSelectedCounter(null);
            }

        }
        else
        {
            SetSelectedCounter(null);
        }
        Debug.Log(SelectedCounter);

    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        ////////////// hal paragraphe t5alik matadhrebch fi 7ajat 
        ///w tod5Ol fi wosthom kel chaba7
        float playerRadius = .7f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);


        if (!canMove)
        {
            //cannot move towards moveDir

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir .x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                //can move only on the x
                moveDir = moveDirX;
            }
            else
            {
                //cannot only move on the x


                //attempt Z movement only 
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    //Can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in any Direction
                }

            }

        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        ////////// menich 3rfa 7ata chm3ntha ama ok


        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.SelectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            SelectedCounter = SelectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()

    { return kitchenObject != null; }





}