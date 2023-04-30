using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PostTransitExec();

public class TruckController : MonoBehaviour
{
    [SerializeField] private Transform boxes;
    [SerializeField] private SceneLoader sceneLoader;
    private Camera cam;

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float leaveDelay = 1f;
    [SerializeField] private Vector2 offsetDistance = new(15f, 0f);
    [SerializeField] private Vector2 truckUpscale = new(4f, 4f);

    private bool isTransitionIn = true;
    private bool isLeaving = false;
    private bool isTransitionOut = false;
    private float timePassed = 0f;
    private Vector2 waitPosition;
    private Vector2 destination;

    private void Awake()
    {
        cam = Camera.main;
        waitPosition = transform.position;
        destination = (Vector2)transform.position + offsetDistance;
        TransitPrep(
            cam.transform.position,
            (Vector2)cam.transform.position + offsetDistance,
            true
        );
    }

    private void Update()
    {
        if (isTransitionIn)
        {
            Transit(PostTransitIn);
            return;
        }
        // Wait untill all boxes are on the truck
        if (!isLeaving)
        {
            isLeaving = boxes.childCount == 0 && !isTransitionOut;
        }
        else if (timePassed >= leaveDelay)
        {
            Transit(PostLeaving);
        }
        else
        {
            timePassed += Time.deltaTime;
        }
        // Drive over the camera
        if (isTransitionOut)
        {
            Transit(PostTransitOut);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTransitionIn) { return; }
        GameObject box = collision.gameObject;
        Destroy(box);
    }

    private void Drive(Vector2 destination)
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            destination,
            moveSpeed * Time.deltaTime
        );
    }

    private float Distance(Vector2 destination)
    {
        return Vector2.Distance(transform.position, destination);
    }

    private void TransitPrep(Vector2 origin, Vector2 dest, bool scaleUp)
    {
        transform.position = origin;
        destination = dest;
        if (scaleUp)
        {
            transform.localScale *= truckUpscale;
        }
        else
        {
            transform.localScale /= truckUpscale;
        }
    }

    private void Transit(PostTransitExec postTransitExec)
    {
        if (Distance(destination) > .1f)
        {
            Drive(destination);
        }
        else
        {
            postTransitExec();
        }
    }

    private void PostTransitIn()
    {
        TransitPrep(waitPosition, waitPosition + offsetDistance, false);
        isTransitionIn = false;
    }

    private void PostLeaving()
    {
        TransitPrep(
            (Vector2)cam.transform.position - offsetDistance,
            cam.transform.position,
            true
        );
        isLeaving = false;
        isTransitionOut = true;
    }

    private void PostTransitOut()
    {
        sceneLoader.Load();
    }
}
