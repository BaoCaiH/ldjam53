using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> hitObject = new List<Collider2D>();
    public Rigidbody2D Collided
    {
        get { return hitObject.Count > 0 ? hitObject[0].attachedRigidbody : null; }
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
