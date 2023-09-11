using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundChanger : MonoBehaviour
{
    public int soundIndex = 0;
    public int blendSeconds = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.SwitchSound(soundIndex, blendSeconds);
            Destroy(this.gameObject);
        }
    }
}
