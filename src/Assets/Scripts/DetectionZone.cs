using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> hitObject = new List<Collider2D>();
    private Collider2D hitbox;

    private void Awake()
    {
        hitbox = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hitObject.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hitObject.Remove(collision);
    }
}
