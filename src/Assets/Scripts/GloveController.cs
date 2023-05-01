using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GloveController : MonoBehaviour
{
    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite inactiveSprite;
    [SerializeField] GameObject glove;

    private Image gloveSprite;

    private void Awake()
    {
        gloveSprite = glove.GetComponent<Image>();
    }

    public void Don()
    {
        gloveSprite.sprite = activeSprite;
    }

    public void Doff()
    {
        gloveSprite.sprite = inactiveSprite;
    }
}
