using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerMoveState : IState
    {
        Controller controller;
        StateMachine csm;

        Movement movement;
        InputController inputController;

        public ControllerMoveState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            movement = controller.Movement;
            inputController = controller.InputController;
        }

        public void Enter(params object[] args)
        {
            movement.State = MovementState.Walk;
        }

        public void ExecuteFrame()
        {
            if (inputController.Run) csm.ChangeState(StateID.ControllerRun);

            movement.MovePosition(inputController.MoveCharDir, Time.deltaTime);
        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {

        }

        public void Exit()
        {

        }
    }
}