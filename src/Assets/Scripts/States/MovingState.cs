using UnityEngine;
using UnityEngine.InputSystem;

class MovingState: PlayerState 
{
    private Vector2 flipper = new(-1f, 1f);
    private Vector2 currentMoveInput;
    private bool isMoving => currentMoveInput.x != 0;

    internal MovingState(Vector2 moveInput)
    {
        currentMoveInput = moveInput;
    }

    public void OnEnter(PlayerController player)
    {
        Debug.Log($"Enter [Moving State] with move {currentMoveInput}!");

        // Set facing direction.
        if ((player.transform.localScale.x * currentMoveInput.x) <= -1)
        {
            // Player has changed direction so here we'll flip the scale.
            player.transform.localScale *= flipper;
            player.facing *= flipper;
        }

        player.animator.SetBool(AnimationParams.MOVE_FLAG, true);
    }

    public PlayerState OnMove(InputAction.CallbackContext context, PlayerController player) 
    {
        Debug.Log($"[Moving State] OnMove: started->{context.started}, performed->{context.performed}, canceled->{context.canceled}");

        currentMoveInput = context.ReadValue<Vector2>();
        if (currentMoveInput.y > 0)
        {
            return new JumpingState(currentMoveInput);
        }
        else if(currentMoveInput.x != 0) 
        {
            return null;
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

    public PlayerState OnJump(InputAction.CallbackContext context, PlayerController player)
    {
        return new JumpingState(currentMoveInput);
    }

    public PlayerState OnUpdate(PlayerController player)
    {
        if (isMoving)
        {
            player.rgbody.velocity = new Vector2(currentMoveInput.x * player.walkSpeed, player.rgbody.velocity.y);
            return null;
        }
        else 
        {
            return new IdlingState();
        }
    }

    public void OnExit(PlayerController player)
    {
        player.animator.SetBool(AnimationParams.MOVE_FLAG, false);
    }
}