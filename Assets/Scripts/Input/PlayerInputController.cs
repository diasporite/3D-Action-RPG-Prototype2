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

        //public void OnMove(InputValue value)
        //{
        //    moveChar = value.Get<Vector2>();
        //}

        //public void OnRotate(InputValue value)
        //{
        //    moveCam = value.Get<Vector2>();
        //}

        //public void OnDpad(InputValue value)
        //{
        //    dpad = value.Get<Vector2>();
        //}

        //public void OnToggleLock(InputValue value)
        //{
        //    if (Controller.CurrentState == StateID.ControllerMove)
        //        Controller.sm.ChangeState(StateID.ControllerStrafe);
        //    else if (Controller.CurrentState == StateID.ControllerStrafe)
        //        Controller.sm.ChangeState(StateID.ControllerMove);
        //}

        //public void OnRun(InputValue value)
        //{
        //    //run = value.isPressed;
        //    if (Controller.CurrentState == StateID.ControllerMove)
        //    {
        //        if (value.isPressed && moveChar != Vector2.zero)
        //            Controller.sm.ChangeState(StateID.ControllerRun);
        //    }
        //    else if (Controller.CurrentState == StateID.ControllerRun)
        //    {
        //        if (!value.isPressed)
        //            Controller.sm.ChangeState(StateID.ControllerMove);
        //    }
        //}

        //public void OnRoll(InputValue value)
        //{
        //    //defend = value.isPressed;

        //    if (CanAct) Controller.AddAction(QueueAction.Defend);
        //}

        //public void OnGuard(InputValue value)
        //{
        //    if (Controller.CurrentState == StateID.ControllerMove || 
        //        Controller.CurrentState == StateID.ControllerStrafe || 
        //        Controller.CurrentState == StateID.ControllerRun)
        //        Controller.sm.ChangeState(StateID.controllerGuard);
        //    else if (Controller.CurrentState == StateID.controllerGuard)
        //    {
        //        if (!value.isPressed)
        //            Controller.sm.ChangeState(StateID.ControllerMove);
        //    }
        //}

        //public void OnAction1(InputValue value)
        //{
        //    // L1
        //    if (CanAct) Controller.AddAction(QueueAction.ActionL1);
        //}

        //public void OnAction2(InputValue value)
        //{
        //    // L2
        //    if (CanAct) Controller.AddAction(QueueAction.ActionL2);
        //}

        //public void OnAction3(InputValue value)
        //{
        //    // R1
        //    if (CanAct) Controller.AddAction(QueueAction.ActionR1);
        //}

        //public void OnAction4(InputValue value)
        //{
        //    //R2
        //    if (CanAct) Controller.AddAction(QueueAction.ActionR2);
        //}

        //public void OnChar1(InputValue value)
        //{

        //}

        //public void OnChar2(InputValue value)
        //{

        //}

        //public void OnChar3(InputValue value)
        //{

        //}

        //public void OnChar4(InputValue value)
        //{

        //}

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