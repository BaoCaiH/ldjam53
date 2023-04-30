using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    private Animator anim;
    private AudioSource sfx;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetTrigger("wobble");
        sfx.Play();
    }
}
