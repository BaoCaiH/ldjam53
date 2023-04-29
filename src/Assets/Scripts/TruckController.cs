using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    [SerializeField] private Transform boxes;
    [SerializeField] private SceneLoader sceneLoader;

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float leaveDelay = 1f;

    private bool isLeaving = false;
    private float timePassed = 0f;
    private Vector2 destination;

    private void Start()
    {
        destination = (Vector2)transform.position + new Vector2(15f, 0f);
    }

    private void Update()
    {
        if (!isLeaving)
        {
            isLeaving = boxes.childCount == 0;
            return;
        }
        if (timePassed >= leaveDelay)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                destination,
                moveSpeed * Time.deltaTime
            );
        }
        else
        {
            timePassed += Time.deltaTime;
        }
        if (Vector2.Distance(transform.position, destination) < .1f)
        {
            sceneLoader.Load();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        GameObject box = collision.gameObject;
        Destroy(box);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
