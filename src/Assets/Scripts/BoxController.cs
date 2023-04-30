using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private Sprite damagedSprite;
    [SerializeField] private int rigidIndex = 2;

    private Rigidbody2D rgbody;
    private Vector2 incomingForce;

    private void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (incomingForce != Vector2.zero) {
            rgbody.velocity = incomingForce;
            incomingForce = Vector2.zero;
        }
    }

    public void Launch(Vector2 force)
    {
        incomingForce = force;
    }

    public void Damage()
    {
        if (rigidIndex > 0)
        {
            rigidIndex--;
        }
        if (rigidIndex == 1)
        {
            GetComponent<SpriteRenderer>().sprite = damagedSprite;
        }
    }

    public bool IsBroken()
    {
        return rigidIndex == 0;
    }
}
