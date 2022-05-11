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

        Vector3 ds;

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
            Command();
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

        void Command()
        {
            ds = inputController.MoveCharXz;

            stamina.Tick();

            if (stamina.Empty)
                csm.ChangeState(StateID.ControllerRecover);
            else if (!inputController.Run())
                csm.ChangeState(StateID.ControllerMove);
            else if (ds == Vector3.zero)
                csm.ChangeState(StateID.ControllerMove);
            else
            {
                foreach (var inp in inputController.actions.Keys)
                {
                    if (inp.Invoke())
                    {
                        controller.AddAction(inputController.actions[inp]);
                        csm.ChangeState(StateID.ControllerAction);
                        return;
                    }
                }

                controller.MoveFree(ds);
            }
        }
    }
}