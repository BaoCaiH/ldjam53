using UnityEngine;
using UnityEngine.InputSystem;

class JumpingState : PlayerState
{
    private float jumpForce;

    // Player can still hold move input while jumping, which can be used to decide the next state.
    private Vector2 currentMoveInput;
    private bool isMoving => currentMoveInput.x != 0;

    private ContactFilter2D castFilter;

    private float groundDistance = 0.05f;
    private RaycastHit2D[] groundHits = new RaycastHit2D[5];

    internal JumpingState(Vector2 moveInput)
    {
        currentMoveInput = moveInput;
    }

    public void OnEnter(PlayerController player)
    {
        Debug.Log("Enter Jumping State!");

        jumpForce = 6f;
        player.animator.SetBool(AnimationParams.JUMP_FLAG, true);
    }

    public PlayerState OnMove(InputAction.CallbackContext context, PlayerController player)
    {
        currentMoveInput = context.ReadValue<Vector2>();
        return null;
    }

    public PlayerState OnAttack(InputAction.CallbackContext context, PlayerController player)
    {
        return null;
    }

    public PlayerState OnJump(InputAction.CallbackContext context, PlayerController player)
    {
        return null;
    }

    public PlayerState OnUpdate(PlayerController player)
    {
        if (jumpForce != 0)
        {
            player.rgbody.velocity = new Vector2(player.rgbody.velocity.x / 2, jumpForce);
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
                if (isMoving)
                {
                    return new MovingState(currentMoveInput);
                }
                else
                {
                    return new IdlingState();
                }
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