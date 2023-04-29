using UnityEngine;
using UnityEngine.InputSystem;

class MovingState: PlayerState 
{
    public void OnEnter(PlayerController player)
    {
        player.animator.SetBool(AnimationParams.MOVE_FLAG, true);
    }

    public PlayerState OnMove(InputAction.CallbackContext context, PlayerController player) 
    {
        Vector2 moveInput = context.ReadValue<Vector2>();

        if (moveInput.x != 0) 
        {
            return new MovingState();
        }
        else
        {
            return new IdlingState();
        }
    }

    public PlayerState OnAttack(InputAction.CallbackContext context, PlayerController player)
    {
        return new AttackingState();
    }
}