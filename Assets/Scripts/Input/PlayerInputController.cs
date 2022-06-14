using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG_Project
{
    public class PlayerInputController : InputController
    {
        public void OnMove(InputValue value)
        {
            moveChar = value.Get<Vector2>();
        }

        public void OnRotate(InputValue value)
        {
            moveCam = value.Get<Vector2>();
        }

        public void OnDpad(InputValue value)
        {
            dpad = value.Get<Vector2>();
        }

        public void OnToggleLock(InputValue value)
        {
            //toggleLock = value.isPressed;

            if (Controller.CurrentState == StateID.ControllerMove)
                Controller.sm.ChangeState(StateID.ControllerStrafe);
            else if (Controller.CurrentState == StateID.ControllerStrafe)
                Controller.sm.ChangeState(StateID.ControllerMove);
        }

        public void OnRun(InputValue value)
        {
            //run = value.isPressed;

            if (Controller.CurrentState == StateID.ControllerMove)
            {
                if (value.isPressed && moveChar != Vector2.zero)
                    Controller.sm.ChangeState(StateID.ControllerRun);
            }
            else if (Controller.CurrentState == StateID.ControllerRun)
            {
                if (!value.isPressed)
                    Controller.sm.ChangeState(StateID.ControllerMove);
            }
        }

        public void OnDefend(InputValue value)
        {
            //defend = value.isPressed;

            if (CanAct) Controller.AddAction(QueueAction.Defend);
        }

        public void OnAction1(InputValue value)
        {
            // L1
            if (CanAct) Controller.AddAction(QueueAction.ActionL1);
        }

        public void OnAction2(InputValue value)
        {
            // L2
            if (CanAct) Controller.AddAction(QueueAction.ActionL2);
        }

        public void OnAction3(InputValue value)
        {
            // R1
            if (CanAct) Controller.AddAction(QueueAction.ActionR1);
        }

        public void OnAction4(InputValue value)
        {
            //R2
            if (CanAct) Controller.AddAction(QueueAction.ActionR2);
        }

        public void OnChar1(InputValue value)
        {

        }

        public void OnChar2(InputValue value)
        {

        }

        public void OnChar3(InputValue value)
        {

        }

        public void OnChar4(InputValue value)
        {

        }

        bool CanAct => Controller.CurrentState != StateID.ControllerFall && 
            Controller.CurrentState != StateID.ControllerRecover && 
            Controller.CurrentState != StateID.ControllerStagger && 
            Controller.CurrentState != StateID.ControllerDeath;
    }
}