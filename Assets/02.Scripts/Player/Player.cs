using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    public float curHp;
    private PlayerController playerController {get; set;}
    private float maxHp = 100;
    // Status
    public void SetController(PlayerController _playerController){
        playerController = _playerController;
    }

    void Awake()
    {

    }

    void Start()
    {
        InitStateMachine();
    }

    void Update()
    {
        stateMachine?.UpdateState();
    }

    void FixedUpdate()
    {
        stateMachine?.FixedUpdateState();
    }

    public void OnUpdateStat(float moveSpeed, int dashCount)
    {

    }

    private void InitStateMachine()
    {
        stateMachine = new StateMachine(StateName.Idle, new IdleState(playerController));
        stateMachine.AddState(StateName.Move, new MoveState(playerController));
        stateMachine.AddState(StateName.Jump, new JumpState(playerController));
    }


}