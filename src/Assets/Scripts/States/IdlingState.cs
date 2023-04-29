using UnityEngine;
using UnityEngine.InputSystem;

class IdlingState: PlayerState 
{
    public void OnEnter(PlayerController player)
    {
        Debug.Log("Enter Idling...");
        player.animator.SetBool(AnimationParams.MOVE_FLAG, false);
    }

    public PlayerState OnMove(InputAction.CallbackContext context, PlayerController player)
    {
        return new MovingState();
    }

    public PlayerState OnAttack(InputAction.CallbackContext context, PlayerController player)
    {
        Debug.Log("Go to winding up...");
        return new AttackingState();
    }
}