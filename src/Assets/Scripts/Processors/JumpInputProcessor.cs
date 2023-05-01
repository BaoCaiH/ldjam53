using UnityEngine;

class JumpInputProcessor : PlayerInputProcessor
{
    private float jumpForce;

    private ContactFilter2D castFilter;

    private float groundDistance = 0.05f;
    private RaycastHit2D[] groundHits = new RaycastHit2D[5];

    public void Enter(PlayerController player)
    {
        jumpForce = player.jumpForce;

        player.animator.SetBool(AnimationParams.JUMP_FLAG, true);
        player.sfxJump.Play();
    }

    public bool Process(PlayerController player)
    {
        if (jumpForce != 0)
        {
            player.rgbody.velocity = new Vector2(player.rgbody.velocity.x, jumpForce);
            jumpForce = 0f;
        }

        return true;
        // if (player.rgbody.velocity.y <= 0)
        // {
        //     // Player is going down. Start casting.
        //     int result = player.capCollider.Cast(Vector2.down, castFilter, groundHits, groundDistance);
        //     bool isGrounded = result > 0;

        //     return isGrounded ? true : false;
        // }
        // else
        // {
        //     return false;
        // }
    }

    public void Exit(PlayerController player)
    {
        player.animator.SetBool(AnimationParams.JUMP_FLAG, false);
    }
}