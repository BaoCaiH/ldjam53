using UnityEngine;
using UnityEngine.InputSystem;

class MoveInputProcessor : PlayerInputProcessor 
{
    private Vector2 flipper = new(-1f, 1f);
    private Vector2 currentMoveInput;

    internal MoveInputProcessor(Vector2 moveInput)
    {
        currentMoveInput = moveInput;
    }

    public virtual void Enter(PlayerController player)
    {
        // Set facing direction.
        if ((player.transform.localScale.x * currentMoveInput.x) <= -1)
        {
            // Player has changed direction so here we'll flip the scale.
            player.transform.localScale *= flipper;
            player.facing *= flipper;
            player.currentWeapon.SetFacing(player.facing);
        }

        player.animator.SetBool(AnimationParams.MOVE_FLAG, true);
    }

    public bool Process(PlayerController player)
    {
        player.rgbody.velocity = new Vector2(currentMoveInput.x * player.currentSpeed, player.rgbody.velocity.y);

        return true;
    }

    public virtual void Exit(PlayerController player)
    {
        player.animator.SetBool(AnimationParams.MOVE_FLAG, false);
    }
}