using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerRunState : IState
    {
        Controller controller;
        StateMachine csm;

        Movement movement;

        Stamina stamina;
        InputController inputController;

        public ControllerRunState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            movement = controller.Movement;

            stamina = controller.Party.Stamina;
            inputController = controller.InputController;
        }

        public void Enter(params object[] args)
        {
            movement.State = MovementState.Run;
            stamina.State = ResourceState.Run;
        }

        public void ExecuteFrame()
        {
            if (stamina.Empty)
                csm.ChangeState(StateID.ControllerRecover);
            else if (!inputController.Run())
                csm.ChangeState(StateID.ControllerMove);
            else
            {
                foreach (var inp in inputController.actions.Keys)
                {
                    if (inp.Invoke())
                    {
                        controller.AddAction(inputController.actions[inp]);
                        csm.ChangeState(StateID.ControllerAction);
                    }
                }

                controller.Move();
            }

            stamina.Tick();
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