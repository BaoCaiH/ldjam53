using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 4f;

    private bool isMoving = false;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Animator anim;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger(AnimationStrings.attackTrigger);
        }
    }
}
