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

        Health health;
        Stamina stamina;
        InputController inputController;

        Vector3 ds;

        public ControllerRunState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            movement = controller.Movement;

            health = controller.Party.Health;
            stamina = controller.Party.Stamina;
            inputController = controller.InputController;
        }

        public void Enter(params object[] args)
        {
            movement.State = MovementState.Run;
            health.State = ResourceState.Run;
            stamina.State = ResourceState.Run;
        }

        public void ExecuteFrame()
        {
            health.Tick();
            stamina.Tick();

            if (!movement.Grounded)
                csm.ChangeState(StateID.ControllerFall);
            else if (stamina.Empty)
                csm.ChangeState(StateID.ControllerRecover);
            else if (ds == Vector3.zero)
                csm.ChangeState(StateID.ControllerMove);
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
            health.Tick();
            stamina.Tick();

            if (stamina.Empty)
                csm.ChangeState(StateID.ControllerRecover);
            else if (ds == Vector3.zero)
                csm.ChangeState(StateID.ControllerMove);
        }
    }
}