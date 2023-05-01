using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject box = collision.gameObject;
        BoxController controller = box.GetComponent<BoxController>();
        if (controller)
        {
            controller.Damage();
            if (controller.IsBroken())
            {
                box.GetComponent<SpriteRenderer>().color = Color.gray;
            }
        }
    }
}
