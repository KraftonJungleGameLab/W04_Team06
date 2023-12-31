using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSwitcher : MonoBehaviour
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

    public void NextScenses(int i)
    {
        nestSceneNum = i;        
        animator.SetTrigger("NextScenes");
    }
}
