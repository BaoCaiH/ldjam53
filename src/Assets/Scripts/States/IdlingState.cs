using UnityEngine;
using UnityEngine.InputSystem;

class IdlingState: PlayerState 
{
    public void OnEnter(PlayerController player)
    {
        Debug.Log("Enter Idling State!");
    }

    public PlayerState OnMove(InputAction.CallbackContext context, PlayerController player)
    {
        Debug.Log($"[Idling State] OnMove: started->{context.started}, performed->{context.performed}, canceled->{context.canceled}");

        Vector2 moveInput = context.ReadValue<Vector2>();
        if (moveInput.x != 0) 
        {
            return new MovingState(moveInput);
        }
        else
        {
            return null;
        }
    }

    public PlayerState OnAttack(InputAction.CallbackContext context, PlayerController player)
    {
        return new AttackingState();
    }

    public PlayerState OnJump(InputAction.CallbackContext context, PlayerController player)
    {
        Debug.Log("IdlingState OnJump...");

        return new JumpingState(Vector2.zero);
    }

    public PlayerState OnUpdate(PlayerController player)
    {
        return null;
    }
    
    public void OnExit(PlayerController player)
    {

    }
}