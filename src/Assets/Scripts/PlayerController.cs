using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rgbody;
    private Animator anim;

    private Vector2 moveInput;
    public float walkSpeed = 5f;

    private void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        if (context.started)
        {
            anim.SetTrigger(AnimationStrings.attackTrigger);
        }
    }
}
