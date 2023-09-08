using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //----------------------------------------------------
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        //DontDestroyOnLoad(this.gameObject);
    }
    //----------------------------------------------------

    public Player player;
    [HideInInspector] public Vector3 savePoint;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        savePoint = player.transform.position;
    }

    public void SetSavePoint(Vector3 savePosition)
    {
        savePoint = savePosition;
    }

    public void RespawnPlayer()
    {
        player.transform.position = savePoint;
        Physics.SyncTransforms();
        player.Init();
    }
}
