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
    [HideInInspector] public Player player;
    [HideInInspector] public Rigidbody rigid;
    public InputData input;

    [Header("Movement")]
    public float moveSpeed;
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


    public Animator anim;

    void Awake()
    {
        player = GetComponent<Player>();
        player.SetController(this);
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerInputCustom.Instance.GetInput(out input);
        //CheckIsGround();
    }

    private void FixedUpdate()
    {
        
    }

    private void CheckIsGround()
    {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(rigid.transform.position, -transform.up, out hit, 2f) && isGroundCollision)
        {
            Debug.DrawRay(rigid.transform.position, -transform.up * 2f, Color.green);
            groundNormal = hit.normal;
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
}