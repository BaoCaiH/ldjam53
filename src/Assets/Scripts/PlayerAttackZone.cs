using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackZone : MonoBehaviour
{
    public Vector2 power = new Vector2(7f, 2f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BoxController boxController = collision.gameObject.GetComponent<BoxController>();
        boxController.Launch(power);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
