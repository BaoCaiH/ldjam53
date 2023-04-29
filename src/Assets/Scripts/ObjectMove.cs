using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    private Vector2 appliedForce;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (appliedForce != Vector2.zero)
        {
            rb.velocity = appliedForce;
            appliedForce = Vector2.zero;
        }
    }

    public void Move(Vector2 force)
    {
        appliedForce = force;
    }
}
