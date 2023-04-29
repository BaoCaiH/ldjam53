using UnityEngine;
using UnityEngine.InputSystem;

class AttackingState: PlayerState 
{
    // private float chargeAngle;

    public void OnEnter(PlayerController player)
    {
        player.animator.SetTrigger(AnimationParams.ATTACK_TRIGGER);
    }

    public PlayerState OnMove(InputAction.CallbackContext context, PlayerController player)
    {
        return null;
    }

    public PlayerState OnAttack(InputAction.CallbackContext context, PlayerController player)
    {
        // chargeAngle = Quaternion.Angle(
        //     new Quaternion(0f, 0f, 0f, player.angleTransform.localRotation.w),
        //     player.angleTransform.localRotation
        // );      
        
        // float chargeForce = player.maxForce * (player.forceTransform.localScale.x / 2);
        // player.hitboxZone.power = new Vector2(
        //     Mathf.Cos(Mathf.PI * chargeAngle / 180f) * chargeForce,
        //     Mathf.Sin(Mathf.PI * chargeAngle / 180f) * chargeForce
        // );
        player.hitboxZone.power = new Vector2(7f, 2f);

        player.animator.SetTrigger(AnimationParams.ATTACK_TRIGGER);

        return new IdlingState();
    }
}