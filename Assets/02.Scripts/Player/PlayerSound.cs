using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] AudioSource footRAudio;
    [SerializeField] AudioSource footLAudio;
    [SerializeField] AudioSource dragAudio;

    public void FootR()
    {
        footRAudio.Play();
    }

    public void FootL()
    {
        footLAudio.Play();
    }

    public void DragOn()
    {
        dragAudio.Play();
    }

    public void DragOff()
    {
        dragAudio.Stop();
    }
}
