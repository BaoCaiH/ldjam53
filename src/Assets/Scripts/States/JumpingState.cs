using UnityEngine;
using UnityEngine.InputSystem;

class JumpingState : PlayerState
{
    private float jumpForce;

    private ContactFilter2D castFilter;

    private float groundDistance = 0.05f;
    private RaycastHit2D[] groundHits = new RaycastHit2D[5];

    public void OnEnter(PlayerController player)
    {
        Debug.Log("Enter Jumping State!");

        jumpForce = player.jumpForce;

        player.animator.SetBool(AnimationParams.JUMP_FLAG, true);
        player.sfxJump.Play();
    }

    public PlayerState OnMove(InputAction.CallbackContext context, PlayerController player)
    {
        return null;
    }

    public PlayerState OnAttack(InputAction.CallbackContext context, PlayerController player)
    {
        //return null;
        return new AttackingState();
    }

    public PlayerState OnJump(InputAction.CallbackContext context, PlayerController player)
    {
        return null;
    }

    public PlayerState OnUpdate(PlayerController player)
    {
        if (jumpForce != 0)
        {
            player.rgbody.velocity = new Vector2(player.rgbody.velocity.x, jumpForce);
            jumpForce = 0f;
        }

        if (player.rgbody.velocity.y <= 0)
        {
            // Player is going down. Start casting.
            int result = player.capCollider.Cast(Vector2.down, castFilter, groundHits, groundDistance);
            bool isGrounded = result > 0;

            Debug.Log($"[MovingState] OnUpdate: isGrounded->{isGrounded}");

            if (isGrounded)
            {
                bool isMoving = player.input.actions["Move"].inProgress;
                return isMoving ? new MovingState() : new IdlingState();
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public void OnExit(PlayerController player)
    {
        player.animator.SetBool(AnimationParams.JUMP_FLAG, false);
    }
}