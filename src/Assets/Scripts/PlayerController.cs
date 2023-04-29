using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject hitbox;
    [SerializeField] private GameObject angleGauge;
    [SerializeField] private GameObject angleMaxGauge;
    [SerializeField] private GameObject forceGauge;
    private Rigidbody2D rgbody;
    private Animator anim;
    private Transform angleTransform;
    private Transform forceTransform;
    private DetectionZone hitboxZone;

    [SerializeField] private float maxForce = 7f;

    private bool isWindingUp = false;
    private bool isCharging = false;
    private float chargeAngle;
    public float walkSpeed = 5f;
    private Vector2 moveInput;

    private void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        angleTransform = angleGauge.transform;
        forceTransform = forceGauge.transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        UpdateMovement();
        UpdateFacingDirection();
    }

    private void UpdateMovement()
    {
        rgbody.velocity = new Vector2(moveInput.x * walkSpeed, rgbody.velocity.y);
    }

    private void UpdateFacingDirection()
    {
        if ((transform.localScale.x * moveInput.x) <= -1) {
            // Player has changed direction so here we'll flip the scale.
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.started) { return; }
        if (!isWindingUp)
        {
            isWindingUp = true;
            anim.SetTrigger(AnimationStrings.attackWindUpTrigger);
        }
        else if (!isCharging)
        {
            chargeAngle = Quaternion.Angle(
                new Quaternion(0f, 0f, 0f, angleTransform.localRotation.w),
                angleTransform.localRotation
            );
            isCharging = true;
            anim.SetTrigger(AnimationStrings.attackChargeTrigger);
        }
        else
        {
            float chargeForce = maxForce * (angleTransform.localScale.x / 2);
            launchVector = new Vector2(
                Mathf.Cos(Mathf.PI * chargeAngle / 180f) * chargeForce,
                Mathf.Sin(Mathf.PI * chargeAngle / 180f) * chargeForce
                );
            isWindingUp = false;
            anim.SetTrigger(AnimationStrings.attackTrigger);
        }
    }
}
