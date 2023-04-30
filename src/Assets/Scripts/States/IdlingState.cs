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

        return new MovingState();
    }

    public PlayerState OnAttack(InputAction.CallbackContext context, PlayerController player)
    {
        return new AttackingState();
    }

    public PlayerState OnJump(InputAction.CallbackContext context, PlayerController player)
    {
        Debug.Log("IdlingState OnJump...");

        return new JumpingState();
    }

    public PlayerState OnUpdate(PlayerController player)
    {
        return null;
    }
    
    public void OnExit(PlayerController player)
    {

    }
}