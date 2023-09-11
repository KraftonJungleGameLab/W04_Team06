using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSwitcher : MonoBehaviour
{
    public Animator animator;
    public bool nextScene = false;
    public bool isSound = true;
    public int nestSceneNum;


    public void Update()
    {
        if (nextScene)
        {
            SceneManager.LoadScene(nestSceneNum);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("NextScenes");
            SoundManager.Instance.EndAudio();
        }
    }
}
