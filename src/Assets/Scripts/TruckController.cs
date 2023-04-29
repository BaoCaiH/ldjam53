using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        GameObject box = collision.gameObject;
        Destroy(box);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
