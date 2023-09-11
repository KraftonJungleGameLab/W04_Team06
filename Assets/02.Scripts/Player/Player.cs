using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    [HideInInspector] public bool isRecoveryOn = true;
    public IInteractableObject interactableObject;
    private PlayerController playerController {get; set;}

    [Header("PlayerHp")]
    public float curHp;
    public float recoveryStartTime = 3.0f;
    public float recoveryHp = 30.0f;
    private float maxHp = 100;
    private Coroutine playerRecoveryCoroutine;
    private bool isRecoveryCoroutineOn;

    public void SetController(PlayerController _playerController){
        playerController = _playerController;
    }

    void Awake()
    {
        GameManager.Instance.InitAction += RespawnPlayer;
    }

    void Start()
    {
        InitStateMachine();
        Init();
    }

    public void Init()
    {
        curHp = maxHp;
        isRecoveryOn = true;
        isRecoveryCoroutineOn = false;
        stateMachine.ChangeState(StateName.Idle);
        playerController.isControllable = true;
        interactableObject = null;
    }

    void Update()
    {
        stateMachine?.UpdateState();
        RecoveryHealth();
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
        if(isRecoveryOn 
            && curHp != maxHp)
        {
            curHp += 30.0f * Time.deltaTime;

            if(curHp > maxHp)
            {
                curHp = maxHp;
            }
        }
    }

    public void DamageHealth(float damage)
    {
        curHp -= damage;
        isRecoveryOn = false;
        if (curHp <= 0f)
        {
            Dead();
            return;
        }

        StartRecoveryCoroutine();
    }

    public void Dead()
    {
        StopRecoveryCoroutine();
        GameManager.Instance.InitAction();
    }

    public void RespawnPlayer()
    {
        transform.position = GameManager.Instance.savePoint;
        Physics.SyncTransforms();
        Init();
    }

    private IEnumerator RecoveryOnAfterSeconds(float seconds)
    {
        isRecoveryCoroutineOn = true;
        yield return new WaitForSeconds(seconds);
        isRecoveryOn = true;
        isRecoveryCoroutineOn = false;
    }

    private void StartRecoveryCoroutine()
    {
        StopRecoveryCoroutine();
        playerRecoveryCoroutine = StartCoroutine(RecoveryOnAfterSeconds(recoveryStartTime));
    }

    private void StopRecoveryCoroutine()
    {
        if (isRecoveryCoroutineOn)
            StopCoroutine(playerRecoveryCoroutine);
    }
}