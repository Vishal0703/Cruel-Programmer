using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Controls controls;
    [HideInInspector]
    public bool jump;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.Jump.performed += _ => Jump(true);
        controls.Player.Jump.canceled += _ => Jump(false);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Jump(bool isJumping)
    {
        jump = isJumping;
    }
}
