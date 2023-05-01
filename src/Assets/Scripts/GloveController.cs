using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GloveController : MonoBehaviour
{
    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite inactiveSprite;
    [SerializeField] GameObject glove;
    [SerializeField] GameObject button;
    [SerializeField] private bool isAvailable = true;

    private Image gloveSprite;
    private Image buttonSprite;

    private void Awake()
    {
        gloveSprite = glove.GetComponent<Image>();
        buttonSprite = button.GetComponent<Image>();
        if (!isAvailable)
        {
            gloveSprite.enabled = false;
            buttonSprite.enabled = false;
        }
    }

    public void Don()
    {
        gloveSprite.sprite = activeSprite;
    }

    public void Doff()
    {
        gloveSprite.sprite = inactiveSprite;
    }

    public bool IsAvailable()
    {
        return isAvailable;
    }
}
