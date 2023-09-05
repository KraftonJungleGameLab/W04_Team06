using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputCustom : MonoBehaviour
{
    //----------------------------------------------------
    private static PlayerInputCustom _instance;
    public static PlayerInputCustom Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        input = new InputData();
        playerInput = new PlayerInputActions();

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

    private InputData input;
    private PlayerInputActions playerInput;

    void Start()
    {
        playerInput.Player.MoveHorizontal.started += OnMoveHorizontal;
        playerInput.Player.MoveHorizontal.canceled += OnMoveHorizontal;

        playerInput.Player.MoveVertical.started += OnMoveVertical;
        playerInput.Player.MoveVertical.canceled += OnMoveVertical;

        playerInput.Player.Move.started += OnMove;
        playerInput.Player.Move.performed += OnMove;
        playerInput.Player.Move.canceled += OnMove;

        playerInput.Player.Look.started += OnLook;
        playerInput.Player.Look.canceled += OnLook;

        playerInput.Player.Jump.started += OnButton;
        playerInput.Player.Jump.canceled += OnButton;

        playerInput.Player.Dash.started += OnButton;
        playerInput.Player.Dash.canceled += OnButton;

        playerInput.Player.Interaction.started += OnButton;
        playerInput.Player.Interaction.canceled += OnButton;

        playerInput.Player.Flash.started += OnButton;
        playerInput.Player.Flash.canceled += OnButton;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void GetInput(out InputData inputData)
    {
        inputData = input;
    }

    private void LateUpdate()
    {
        input.buttonsDown = 0x00;
        input.buttonsUp = 0x00;
    }

    public void OnMoveHorizontal(InputAction.CallbackContext context)
    {
        input.direction.x = context.ReadValue<float>();
    }

    public void OnMoveVertical(InputAction.CallbackContext context)
    {
        input.direction.z = context.ReadValue<float>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        input.direction.x = direction.x;
        input.direction.z = direction.y;
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        input.look = context.ReadValue<Vector2>();
    }
    
    public void OnButton(InputAction.CallbackContext context)
    {
        ButtonInputOnOff(byte.Parse(Regex.Replace(context.action.processors, @"\D", "")), context.ReadValueAsButton());
    }

    public void ButtonInputOnOff(byte bitPosition, bool isOn)
    {
        if (isOn)
        {
            input.buttons |= bitPosition;
            input.buttonsDown |= bitPosition;
        }
        else
        {
            input.buttons &= (byte)(byte.MaxValue - bitPosition);
            input.buttonsUp |= bitPosition;
        }
    }
}
