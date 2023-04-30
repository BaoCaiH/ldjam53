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
    internal Animator animator;
    internal Transform angleTransform;
    internal Transform forceTransform;
    internal PlayerAttackZone hitboxZone;

    [SerializeField] 
    internal float maxForce = 7f;

    private PlayerState currentState;

    private float chargeAngle;
    public float walkSpeed = 5f;
    private Vector2 moveInput;
    public Vector2 power;

    private void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitboxZone = hitbox.GetComponent<PlayerAttackZone>();
        angleTransform = angleGauge.transform;
        forceTransform = forceGauge.transform;
        currentState = new IdlingState();
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

        PlayerState state = currentState.OnMove(context, this);
        if (state != null)
        {
            state.OnEnter(this);
            currentState = state;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.started) { return; }
        
        PlayerState state = currentState.OnAttack(context, this);
        if (state != null)
        {
            state.OnEnter(this);
            currentState = state;
        }
    }
}
