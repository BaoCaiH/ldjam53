using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController : MonoBehaviour
{
    [SerializeField] Transform startBox;
    [SerializeField] Vector2 startBoxPosition = new(5.5f, 4f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        startBox.position = startBoxPosition;
    }
}
