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
    internal TouchingDirections touchingDirs;

    [SerializeField] internal float maxForce = 7f;
    [SerializeField] internal float jumpForce = 10f;
    [SerializeField] internal float walkSpeed = 4f;
    [SerializeField] internal float runSpeed = 8f;
    [SerializeField] internal float currentSpeed = 4f;

    internal Vector2 facing = new(1f, 1f);
    private Vector2 moveInput;
    public Vector2 power;

    private List<PlayerInputProcessor> inputProcessors;

    private void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        capCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        hitboxZone = hitbox.GetComponent<PlayerAttackZone>();
        touchingDirs = GetComponent<TouchingDirections>();
        angleTransform = angleGauge.transform;
        forceTransform = forceGauge.transform;
        inputProcessors = new List<PlayerInputProcessor>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        for (int i = inputProcessors.Count - 1; i >= 0; i--)
        {
            if (!inputProcessors[i].Process(this))
            {
                break;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerInputProcessor newState = new MoveInputProcessor(context.ReadValue<Vector2>());
            newState.Enter(this);

            inputProcessors.Add(newState);
        }
        else if (context.canceled)
        {
            inputProcessors.RemoveAll((state) => {
                if (state is MoveInputProcessor)
                {
                    state.Exit(this);
                    return true;
                }
                else 
                {
                    return false;
                }
            });
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerInputProcessor newState = new AttackInputProcessor();
            newState.Enter(this);

            inputProcessors.Add(newState);
        }
        else if (context.canceled)
        {
            inputProcessors.RemoveAll((state) => {
                if (state is AttackInputProcessor)
                {
                    state.Exit(this);
                    return true;
                }
                else 
                {
                    return false;
                }
            });
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (touchingDirs.IsGrounded)
            {
                PlayerInputProcessor newState = new JumpInputProcessor();
                newState.Enter(this);

                inputProcessors.Add(newState);
            }
        }
        else if (context.canceled)
        {
            inputProcessors.RemoveAll((state) => {
                if (state is JumpInputProcessor)
                {
                    state.Exit(this);
                    return true;
                }
                else 
                {
                    return false;
                }
            });
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PlayerInputProcessor newState = new RunInputProcessor();
            newState.Enter(this);

            inputProcessors.Add(newState);
        }
        else if (context.canceled)
        {
            inputProcessors.RemoveAll((state) => {
                if (state is RunInputProcessor)
                {
                    state.Exit(this);
                    return true;
                }
                else 
                {
                    return false;
                }
            });
        }
    }
}
