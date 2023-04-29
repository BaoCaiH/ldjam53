using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] Transform startBox;
    [SerializeField] Vector2 startBoxPosition = new Vector2(5.5f, 4f);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        startBox.position = startBoxPosition;
    }
}
