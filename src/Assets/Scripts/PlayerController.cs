using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject hitbox;
    private Rigidbody2D rgbody;
    private Animator anim;
    private DetectionZone hitboxZone;

    [SerializeField] private Vector2 launchVector = new Vector2(7f, 2f);
    public float walkSpeed = 5f;
    private Vector2 moveInput;

    private void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hitboxZone = hitbox.GetComponent<DetectionZone>();
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

        if (hitboxZone.Collided)
        {
            launch(hitboxZone.Collided);
        }
    }

    private void launch(Rigidbody2D rb)
    {
        rb.velocity = launchVector;
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
