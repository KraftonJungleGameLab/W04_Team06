using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Player player { get; private set; }
    [HideInInspector] public CharacterController controller { get; private set; }
    [HideInInspector] public Animator animator { get; private set; }
     public InputData input;

    [Header("Movement")]
    public float gravity = 0.7f;
    public float gravityAcceleration = 1.0f;
    public float moveSpeed = 11.0f;
    public float jumpImpulse = 0.25f;
    public float rotationVelocity = 20.0f;
    [HideInInspector] public Vector3 moveVelocity;
    [HideInInspector] public Vector3 gravityVelocity;
    private Vector3 finalVelocity;
    [HideInInspector] public Vector3 inputDirection;
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public bool isGrounded;

    private Camera playerCamera;

    void Awake()
    {
        player = GetComponent<Player>();
        player.SetController(this);
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        playerCamera = Camera.main;
    }

    private void Start()
    {
        moveVelocity = Vector3.zero;
        gravityVelocity = Vector3.zero;
        finalVelocity = Vector3.zero;
    }

    private void Update()
    {
        PlayerInputCustom.Instance.GetInput(out input);
        CalcMoveDirection();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        FixedUpdatePlayerMove();
    }

    private void CalculateGravity()
    {
        if (isGrounded == false)
        {
            if (gravityVelocity.y > gravity)
            {
                gravityVelocity -= Vector3.up * gravityAcceleration * Time.fixedDeltaTime;
            }
            else
            {
                gravityVelocity -= Vector3.up * gravity * Time.fixedDeltaTime;
            }
        }
    }

    private void CheckHead()
    {
        float maxDistance = 0.5f;
        Debug.DrawRay(transform.position, Vector3.down * maxDistance, Color.red);
        Ray ray = new Ray(this.transform.position, Vector3.up);
        gravityVelocity.y = -0.1f;
    }

    private void CheckGrounded()
    {
        if (controller.isGrounded)
        {
            isGrounded = true;
            return;
        }

        float maxDistance = 0.5f;
        Debug.DrawRay(transform.position, Vector3.down * maxDistance, Color.red);
        Ray ray = new Ray(this.transform.position, Vector3.down);
        isGrounded = Physics.Raycast(ray, maxDistance);
    }

    private void FixedUpdatePlayerMove()
    {
        CalculateGravity();
        finalVelocity = moveVelocity + gravityVelocity;
        controller.Move(finalVelocity);
    }

    public void CalcMoveDirection()
    {
        inputDirection = new Vector3(input.direction.x, 0.0f, input.direction.z).normalized;

        if(inputDirection.magnitude > 0.1f)
        {
            Vector3 forwardDirection = playerCamera.transform.forward;
            Vector3 rightDirection = playerCamera.transform.right;
            Vector3 rotateDirection = (forwardDirection * input.direction.z + rightDirection * input.direction.x).normalized;
            rotateDirection.y = 0f;
            moveDirection = rotateDirection;
        }
    }
    
    public void MoveFixedUpdate()
    {
        if (inputDirection.magnitude > 0.1f)
        {
            moveVelocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
        }
    }

    public void RotateFixedUpdate()
    {
        if (inputDirection.magnitude > 0.1f)
        {
            Quaternion moveRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            animator.transform.localRotation = Quaternion.Lerp(animator.transform.localRotation, moveRotation, rotationVelocity * Time.fixedDeltaTime);
        }
    }
}