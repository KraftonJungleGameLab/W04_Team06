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
    public float moveSpeed = 3.0f;
    public float sprintSpeed;
    public float jumpSpeed;
    public Vector3 groundNormal;
    public bool isGround;
    public bool isGroundCollision;
    public float targetRotation = 0.0f;
    public float rotationSmoothTime = 0.12f;
    public float rotationVelocity;
    public float superJumpTimer = 0f;
    public float superJumpTime = 2f;
    public float communicateTime = 1f;
    public float communicateTimer = 0f;

    void Awake()
    {
        player = GetComponent<Player>();
        player.SetController(this);
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        PlayerInputCustom.Instance.GetInput(out input);
    }

    private void FixedUpdate()
    {

    }
}