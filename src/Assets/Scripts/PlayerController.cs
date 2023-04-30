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
    [SerializeField] internal AudioSource sfxAttack;
    [SerializeField] internal AudioSource sfxJump;
    internal Rigidbody2D rgbody;
    internal CapsuleCollider2D capCollider;
    internal Animator animator;
    internal PlayerInput input;
    internal Transform angleTransform;
    internal Transform forceTransform;
    internal PlayerAttackZone hitboxZone;

    [SerializeField] internal float maxForce = 7f;
    [SerializeField] internal float jumpForce = 10f;
    [SerializeField] internal float walkSpeed = 4f;
    [SerializeField] internal float runSpeed = 8f;

    internal Vector2 facing = new(1f, 1f);
    private Vector2 moveInput;
    public Vector2 power;

    private PlayerState currentState;

    private void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
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

    public void OnRun(InputAction.CallbackContext context)
    {
        PlayerState state = currentState.OnRun(context, this);
        TransitionToState(state);
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
