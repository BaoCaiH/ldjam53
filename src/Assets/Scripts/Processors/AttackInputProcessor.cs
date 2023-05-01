using UnityEngine;
using UnityEngine.InputSystem;

class AttackInputProcessor : PlayerInputProcessor
{
    public void Enter(PlayerController player)
    {
        //player.hitboxZone.power = new Vector2(7f, 2f) * player.facing;

        player.animator.SetTrigger(AnimationParams.ATTACK_TRIGGER);

        player.sfxAttack.Play();
    }

    // public PlayerState OnAttack(InputAction.CallbackContext context, PlayerController player) {
    //     // chargeAngle = Quaternion.Angle(
    //     //     new Quaternion(0f, 0f, 0f, player.angleTransform.localRotation.w),
    //     //     player.angleTransform.localRotation
    //     // );      

    //     // float chargeForce = player.maxForce * (player.forceTransform.localScale.x / 2);
    //     // player.hitboxZone.power = new Vector2(
    //     //     Mathf.Cos(Mathf.PI * chargeAngle / 180f) * chargeForce,
    //     //     Mathf.Sin(Mathf.PI * chargeAngle / 180f) * chargeForce
    //     // );
    //     return null;
    // }


    public bool Process(PlayerController player)
    {
        return true;
    }

    public void Exit(PlayerController player)
    {

    }
}