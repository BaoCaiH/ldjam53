using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    private Rigidbody2D rgbody;

    private Vector2 incomingForce;

    private void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
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
}
