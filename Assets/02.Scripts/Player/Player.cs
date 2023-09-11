using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    [HideInInspector] public bool isRecoveryOn = true;
    public IInteractableObject interactableObject;
    public PlayerDeadEffect playerDeadEffect;
    private PlayerController playerController {get; set;}
    [SerializeField] private Material bodyMaterial;

    [Header("PlayerHp")]
    public float curHp;
    public float recoveryStartTime = 3.0f;
    public float recoveryHp = 30.0f;
    public bool isDead = false;
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
        isDead = false;
        curHp = maxHp;
        isRecoveryOn = true;
        isRecoveryCoroutineOn = false;
        stateMachine.ChangeState(StateName.Idle);
        playerController.isControllable = true;
        interactableObject = null;
        ChangeBodyColor();
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

    private void InitAnimator()
    {
        playerController.animator.SetBool("Move", false);
        playerController.animator.SetBool("Jump", false);
        playerController.animator.SetBool("Grab", false);

    }

    public void RecoveryHealth()
    {
        if(isRecoveryOn 
            && curHp != maxHp)
        {
            curHp += 30.0f * Time.deltaTime;
            ChangeBodyColor();

            if (curHp > maxHp)
            {
                curHp = maxHp;
            }
        }
    }

    public void DamageHealth(float damage)
    {
        if (isDead)
            return;

        curHp -= damage;
        ChangeBodyColor();
        isRecoveryOn = false;
        if (curHp <= 0f)
        {
            Dead();
            return;
        }

        StartRecoveryCoroutine();
    }

    private void ChangeBodyColor()
    {
        float hpRate = (1 - curHp / maxHp);
        bodyMaterial.color = new Color(hpRate, hpRate, hpRate, 1.0f);
    }

    public void Dead()
    {
        StopRecoveryCoroutine();
        playerDeadEffect.PlayDeadEffect();
        isDead = true;
    }

    public void RespawnPlayer()
    {
        transform.position = GameManager.Instance.savePoint;
        Physics.SyncTransforms();
        Init();
        InitAnimator();
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