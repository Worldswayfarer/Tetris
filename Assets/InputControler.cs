using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

[Serializable]
public class MoveInputEvent : UnityEvent<Vector2> { }

public class InputControler : MonoBehaviour
{
    Controls controls;
    public MoveInputEvent moveInputEvent;


    private void Awake()
    {
        controls = new Controls();
    }


    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Move.started += OnMovePerformed;
        controls.Player.Move.canceled += OnMovePerformed;

        controls.Player.Rotate.performed += OnRotatePerformed;

        controls.Player.SlideDown.started += OnSlidePerformed;
        controls.Player.SlideDown.canceled += OnSlidePerformed;
    }

    private void OnSlidePerformed(InputAction.CallbackContext context)
    {
        moveInputEvent.Invoke(Vector2.down);
    }

    private void OnRotatePerformed(InputAction.CallbackContext obj)
    {
        print("Spin");
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        print("why");
        Vector2 moveInput = context.ReadValue<Vector2>();
        moveInputEvent.Invoke(moveInput);
    }
}
