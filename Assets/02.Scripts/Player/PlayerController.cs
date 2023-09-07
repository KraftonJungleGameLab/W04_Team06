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
    [HideInInspector] public Vector3 moveVelocity;
    [HideInInspector] public Vector3 gravityVelocity;
    private Vector3 finalVelocity;
    [HideInInspector] public bool isGrounded;

    void Awake()
    {
        player = GetComponent<Player>();
        player.SetController(this);
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
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
            if (gravityVelocity.y > gravity && gravityVelocity.y < 0)
            {
                gravityVelocity -= Vector3.up * gravityAcceleration * Time.fixedDeltaTime;
            }
            else
            {
                gravityVelocity -= Vector3.up * gravity * Time.fixedDeltaTime;
            }
        }
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
}