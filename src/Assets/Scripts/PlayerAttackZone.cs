using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackZone : MonoBehaviour
{
    public Vector2 power = Vector2.zero;

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            BoxController boxController = collision.gameObject.GetComponent<BoxController>();
            boxController.Launch(power);
            power = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
