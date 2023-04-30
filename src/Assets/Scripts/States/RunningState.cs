using UnityEngine;
using UnityEngine.InputSystem;

class RunningState: MovingState
{
    public override void OnEnter(PlayerController player)
    {
        Debug.Log("Enter [Running State]!");
        base.OnEnter(player);
        player.animator.SetBool(AnimationParams.RUN_FLAG, true);
    }

    // public PlayerState OnMove(InputAction.CallbackContext context, PlayerController player) {
    //     return null;
    // }
    // public PlayerState OnAttack(InputAction.CallbackContext context, PlayerController player)
    // {
    //     return null;
    // }
    // public PlayerState OnJump(InputAction.CallbackContext context, PlayerController player)
    // {
    //     return null;
    // }

    public override PlayerState OnRun(InputAction.CallbackContext context, PlayerController player)
    {
        Debug.Log($"[Running State] OnRun: started->{context.started}, performed->{context.performed}, canceled->{context.canceled}");
        if (context.canceled)
        {
            // bool isMoving = player.input.actions["Move"].inProgress;
            // return isMoving ? new MovingState() : new IdlingState();
            return new MovingState();
        }
        else
        {
            return null;
        }
    }

    // public new PlayerState OnUpdate(PlayerController player)
    // {
    //     return null;
    // }

    public override void OnExit(PlayerController player)
    {
        player.animator.SetBool(AnimationParams.RUN_FLAG, false);
    }

    protected override float GetMoveSpeed(PlayerController player)
    {
        return player.walkSpeed * 2;
    }
}