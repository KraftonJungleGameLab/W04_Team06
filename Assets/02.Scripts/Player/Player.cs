using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    public float curHp;
    public float recoveryTime = 3.0f;
    public float recoveryHp = 30.0f;
    [HideInInspector] public bool isRecoveryOn = true;
    [HideInInspector] public IInteractableObject interactableObject;
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
        curHp = maxHp;
    }

    void Update()
    {
        stateMachine?.UpdateState();
        RecoveryHealth();
        if(curHp <= 0)
        {

        }
    }

    void FixedUpdate()
    {
        stateMachine?.FixedUpdateState();
    }

    private void InitStateMachine()
    {
        stateMachine = new StateMachine(StateName.Idle, new IdleState(playerController));
        stateMachine.AddState(StateName.Move, new MoveState(playerController));
        stateMachine.AddState(StateName.Jump, new JumpState(playerController));
        stateMachine.AddState(StateName.GrabIdle, new GrabIdleState(playerController));
        stateMachine.AddState(StateName.GrabMove, new GrabMoveState(playerController));
        stateMachine.AddState(StateName.EquipIdle, new EquipIdleState(playerController));
        stateMachine.AddState(StateName.EquipMove, new EquipMoveState(playerController));
    }

    public void RecoveryHealth()
    {
        if(isRecoveryOn)
        {
            curHp += 30.0f * Time.deltaTime;

            if(curHp > maxHp)
            {
                curHp = maxHp;
            }
        }
    }
}