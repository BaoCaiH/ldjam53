using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Player components.
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

    // Player properties.
    // Moving properties.
    [SerializeField] internal float walkSpeed = 4f;
    [SerializeField] internal float runSpeed = 8f;
    [SerializeField] internal float currentSpeed = 4f;
    // Jump properties.
    [SerializeField] internal float jumpForce = 10f;
    [SerializeField] internal int remainingJump = 2;
    // Attack properties.
    [SerializeField] internal float maxForce = 7f;
    internal Vector2 facing = new(1f, 1f);
    [SerializeField] internal Weapon currentWeapon;
    // Glove properties
    internal GloveController[] gloveControllers;


    // Internal handling logic.
    // Input processors
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
        currentWeapon = new NormalGlove();
        inputProcessors = new List<PlayerInputProcessor>();
        gloveControllers = FindObjectsOfType(typeof(GloveController)) as GloveController[];
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
            inputProcessors.RemoveAll((state) =>
            {
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

            if (currentWeapon is ResetGlove)
            {
                currentWeapon.Apply(gameObject);
            }
            else 
            {
                inputProcessors.Add(newState);
            }
        }
        else if (context.canceled)
        {
            inputProcessors.RemoveAll((state) =>
            {
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
                remainingJump = 2;
            }

            if (remainingJump > 0)
            {
                PlayerInputProcessor newState = new JumpInputProcessor();
                newState.Enter(this);

                inputProcessors.Add(newState);

                remainingJump--;
            }
        }
        else if (context.canceled)
        {
            inputProcessors.RemoveAll((state) =>
            {
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
            inputProcessors.RemoveAll((state) =>
            {
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

    public void OnWear(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentWeapon.OnDetach(this);

            int.TryParse(context.control.name, out int keyPressed);

            if (keyPressed == 0 && ActiveGloves() > 1)
            {
                currentWeapon = new ResetGlove();
                hitboxZone.SetSprite("reset");
            }
            else if (keyPressed == 1 && keyPressed < ActiveGloves())
            {
                currentWeapon = new NormalGlove();
                hitboxZone.SetSprite("normal");
            }
            else if (keyPressed == 2 && keyPressed < ActiveGloves())
            {
                currentWeapon = new SkyGlove();
                hitboxZone.SetSprite("sky");
            }
            else if (keyPressed == 3 && keyPressed < ActiveGloves())
            {
                currentWeapon = new BackwardGlove();
                hitboxZone.SetSprite("backward");
            }
            else if (keyPressed == 4 && keyPressed < ActiveGloves())
            {
                currentWeapon = new HeavyGlove();
                hitboxZone.SetSprite("heavy");
            }
            else
            {
                currentWeapon = new NormalGlove();
                hitboxZone.SetSprite("normal");
            }

            currentWeapon.OnAttach(this);
            currentWeapon.SetFacing(facing);
        }
    }

    private int ActiveGloves()
    {
        int cnt = 0;
        for (int i = 0; i < gloveControllers.Length; i++)
        {
            cnt += gloveControllers[i].IsAvailable() ? 1 : 0;
        }
        return cnt;
    }
}
