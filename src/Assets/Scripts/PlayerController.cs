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
    internal Rigidbody2D rgbody;
    internal CapsuleCollider2D capCollider;
    internal Animator animator;
    internal Transform angleTransform;
    internal Transform forceTransform;
    internal PlayerAttackZone hitboxZone;

    [SerializeField] 
    internal float maxForce = 7f;

    private Vector2 moveInput;
    public Vector2 power;

    private PlayerState currentState;

    private void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
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
        UpdateState();
    }

    private void UpdateState()
    {
        PlayerState state = currentState.OnUpdate(this);
        TransitionToState(state);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        PlayerState state = currentState.OnMove(context, this);
        TransitionToState(state);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayerState state = currentState.OnAttack(context, this);
            TransitionToState(state);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) 
        {
            PlayerState state = currentState.OnJump(context, this);
            TransitionToState(state);
        }
    }

    private void TransitionToState(PlayerState newState)
    {
        if (newState != null)
        {
            currentState.OnExit(this);
            newState.OnEnter(this);

            currentState = newState;
        }
    }
}
