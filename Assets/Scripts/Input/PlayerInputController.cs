using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG_Project
{
    public class PlayerInputController : InputController, PlayerControls.IPlayerActions
    {
        PlayerControls playerControls;

        bool CanAct => Controller.CurrentState != StateID.ControllerFall &&
            Controller.CurrentState != StateID.ControllerRecover &&
            Controller.CurrentState != StateID.ControllerStagger &&
            Controller.CurrentState != StateID.ControllerDeath;

        private void Awake()
        {
            playerControls = new PlayerControls();
            playerControls.Player.SetCallbacks(this);
        }

        private void OnEnable()
        {
            playerControls.Player.Enable();
        }

        private void OnDisable()
        {
            playerControls.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveChar = context.ReadValue<Vector2>();
        }

        public void OnRotate(InputAction.CallbackContext context)
        {
            MoveCam = context.ReadValue<Vector2>();
        }

        public void OnDpad(InputAction.CallbackContext context)
        {
            Dpad = context.ReadValue<Vector2>();

            if (context.performed) InvokeDpad(Dpad);
        }

        public void OnAction1(InputAction.CallbackContext context)
        {
            if (context.performed) InvokeAction(0);
        }

        public void OnAction2(InputAction.CallbackContext context)
        {
            if (context.performed) InvokeAction(1);
        }

        public void OnAction3(InputAction.CallbackContext context)
        {
            if (context.performed) InvokeAction(2);
        }

        public void OnAction4(InputAction.CallbackContext context)
        {
            if (context.performed) InvokeAction(3);
        }

        public void OnGuard(InputAction.CallbackContext context)
        {
            if (context.canceled) InvokeRoll();
            else if (context.performed) InvokeGuard();
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            if (context.canceled) InvokeWalk(MoveChar);
            else if (context.performed) InvokeRun(MoveChar);
        }

        public void OnToggleLock(InputAction.CallbackContext context)
        {
            if (context.performed) InvokeLock();
        }
    }
}