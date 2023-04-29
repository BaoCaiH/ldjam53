using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCredit : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private float leaveDelay = 1f;

    private bool isLoading = false;
    private float timePassed = 0f;

    private void Update()
    {
        if (!isLoading) { return; }
        if (timePassed >= leaveDelay)
        {
            isLoading = false;
            sceneLoader.Load();
        }
        else
        {
            timePassed += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isLoading = true;
    }
}
