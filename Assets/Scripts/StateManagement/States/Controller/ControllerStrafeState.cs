using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerStrafeState : IState
    {
        Controller controller;
        StateMachine csm;

        Movement movement;
        InputController inputController;

        public ControllerStrafeState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            movement = controller.Movement;
            inputController = controller.InputController;
        }

        public void Enter(params object[] args)
        {
            controller.Pivot.ToggleLock(true);
            movement.State = MovementState.Walk;
        }

        public void ExecuteFrame()
        {
            if (inputController.ToggleLock()) csm.ChangeState(StateID.ControllerMove);
            else
            {
                foreach (var inp in inputController.actions.Keys)
                {
                    if (inp.Invoke())
                    {
                        controller.ActionQueue.AddAction(inputController.actions[inp]);
                        csm.ChangeState(StateID.ControllerAction);
                    }
                }

                movement.MovePosition(inputController.MoveCharDir, Time.deltaTime);
            }
        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {

        }

        public void Exit()
        {
            controller.Pivot.ToggleLock(false);
        }
    }
}