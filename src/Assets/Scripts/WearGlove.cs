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
    }

    public void OnWear(InputAction.CallbackContext context)
    {
        int.TryParse(context.control.name, out int keyPressed);
        foreach (GloveController glove in gloves)
        {
            glove.Doff();
        }
        if (keyPressed < gloves.Length && keyPressed > -1 && gloves[keyPressed].IsAvailable())
        {
            Debug.Log($"Don {gloves[keyPressed]}");
            gloves[keyPressed].Don();
        }
        else
        {
            gloves[1].Don();
        };
    }
}
