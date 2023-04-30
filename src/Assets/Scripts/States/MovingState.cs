using UnityEngine;
using UnityEngine.InputSystem;

class MovingState: PlayerState 
{
    private Vector2 flipper = new(-1f, 1f);
    private Vector2 currentMoveInput;
    private bool isMoving => currentMoveInput.x != 0;

    public virtual void OnEnter(PlayerController player)
    {
        currentMoveInput = player.input.actions["Move"].ReadValue<Vector2>();

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
            return new JumpingState();
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

    public virtual PlayerState OnRun(InputAction.CallbackContext context, PlayerController player)
    {
        if (context.started)
        {
            return new RunningState();
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
        return new JumpingState();
    }

    public PlayerState OnUpdate(PlayerController player)
    {
        bool isRunning = player.input.actions["Run"].inProgress;

        if (isMoving)
        {
            player.rgbody.velocity = new Vector2(currentMoveInput.x * GetMoveSpeed(player), player.rgbody.velocity.y);
            return isRunning ? new RunningState() : null;
        }
        else 
        {
            return new IdlingState();
        }
    }

    public virtual void OnExit(PlayerController player)
    {
        player.animator.SetBool(AnimationParams.MOVE_FLAG, false);
    }

    protected virtual float GetMoveSpeed(PlayerController player)
    {
        return player.walkSpeed;
    }
}