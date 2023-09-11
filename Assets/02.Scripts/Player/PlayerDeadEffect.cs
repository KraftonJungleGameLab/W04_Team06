using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadEffect : MonoBehaviour
{
    private Animator animator;
    private AudioSource deadAudio;

    void Start()
    {
        animator = GetComponent<Animator>();
        deadAudio = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        deadAudio.Play();
    }

    public void PlayDeadEffect()
    {
        animator.SetTrigger("DieTrigger");
    }
}
