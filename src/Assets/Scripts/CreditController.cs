using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditController : MonoBehaviour
{
    [SerializeField] private GameObject startBox;
    [SerializeField] private GameObject truck;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        truck.GetComponent<SceneLoader>().sceneName = "Credits";
        Destroy(startBox);
    }
}
