using UnityEngine;
using System;


public class GameInput : MonoBehaviour

{

    public event EventHandler OnInteractAction; // this is to make the player recognize if we cliked on E
    public event EventHandler OnInteractAlternateAction; // this is to make the player recognize if we cliked on F
    private PlayerInputActions playerInputActions;

    private void Awake()
    {

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        //listen to it first
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;

    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);

    }



    //here to know when the player in clicking
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //instead of writing Onteraction != null we just use that sentence
        //its called no conditional operator it execute from left to right
       OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetMovementVectorNormalized()
    {

       Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
