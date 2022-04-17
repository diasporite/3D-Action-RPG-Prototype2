using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerFallState : IState
    {
        Controller controller;
        StateMachine csm;

        Movement movement;

        public ControllerFallState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;
        }

        public void Enter(params object[] args)
        {
            controller.Model.SetAnimFalling(true);
        }

        public void ExecuteFrame()
        {
            movement.State = MovementState.Fall;
        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {

        }

        public void Exit()
        {
            controller.Model.SetAnimFalling(false);
        }
    }
}