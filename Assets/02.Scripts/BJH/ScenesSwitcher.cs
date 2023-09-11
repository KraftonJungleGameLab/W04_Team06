using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSwitcher : MonoBehaviour
{
    public Animator animator;
    public bool nextScene = false;
    public bool isSound = true;
    public int nestSceneNum;

    private void Start()
    {
        Invoke("PlayerControllerOff", 0.2f);
        Screen.SetResolution(1920, 1080, true);
    }

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
    public void GameStart()
    {
        animator.SetTrigger("Start");
    }
    public void EndGame()
    {
        Application.Quit();
    }

    public void PlayerControllerOff()
    {
        GameManager.Instance.player.GetComponent<PlayerController>().isControllable = false;
    }

    public void PlayerControllerOn()
    {
        GameManager.Instance.player.GetComponent<PlayerController>().isControllable = true;
    }
}
