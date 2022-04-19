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

        public void OnToggleLock(InputValue value)
        {
            toggleLock = value.isPressed;
        }

        public void OnRun(InputValue value)
        {
            run = value.isPressed;
        }

        public void OnDefend(InputValue value)
        {
            defend = value.isPressed;
        }

        public void OnActionL1(InputValue value)
        {
            actionL1 = value.isPressed;
        }

        public void OnActionL2(InputValue value)
        {
            actionL2 = value.isPressed;
        }

        public void OnActionR1(InputValue value)
        {
            actionR1 = value.isPressed;
        }

        public void OnActionR2(InputValue value)
        {
            actionR2 = value.isPressed;
        }
    }
}