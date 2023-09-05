using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerNewInput : MonoBehaviour
{
    private CharacterController controller;
    public Animator animator;
    public float moveSpeed = 3.0f;

    private Vector2 moveInput;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // MoveInput을 이용하여 입력을 받음
        Vector3 moveDirection = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * 10.0f);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            controller.SimpleMove(moveDirection * moveSpeed);
            animator.SetBool("Move", true); // Move 트리거 파라미터를 true로 설정
        }
        else
        {
            controller.SimpleMove(Vector3.zero);
            animator.SetBool("Move", false); // Move 트리거 파라미터를 false로 설정
        }
    }

    
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}