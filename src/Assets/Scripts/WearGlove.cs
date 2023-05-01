using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WearGlove : MonoBehaviour
{
    private GloveController[] gloves;

    private void Awake()
    {
        gloves = GetComponentsInChildren<GloveController>();
        foreach (GloveController glove in gloves)
        {
            Debug.Log(glove);
        }
    }

    public void OnWear(InputAction.CallbackContext context)
    {
        int.TryParse(context.control.name, out int keyPressed);
        foreach (GloveController glove in gloves)
        {
            glove.Doff();
        }
        if (keyPressed < 4)
        {
            Debug.Log($"Don {gloves[keyPressed]}");
            gloves[keyPressed].Don();
        };
    }
}
