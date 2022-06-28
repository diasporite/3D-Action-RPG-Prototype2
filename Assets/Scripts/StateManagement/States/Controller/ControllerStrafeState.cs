using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerStrafeState : IState
    {
        Controller controller;
        StateMachine csm;

        Health health;
        Stamina stamina;
        InputController inputController;

        Movement movement;

        public ControllerStrafeState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            health = controller.Party.Health;
            stamina = controller.Party.Stamina;
            inputController = controller.InputController;

            movement = controller.Movement;
        }

        public void Enter(params object[] args)
        {
            movement.State = MovementState.Walk;

            controller.Model.PlayAnimationFade(controller.strafeHash, 0, false);
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
            health.Tick();
            stamina.Tick();

            if (controller.TargetSphere.NoTargets)
            {
                controller.TargetSphere.Active = false;
                csm.ChangeState(StateID.ControllerMove);
            }
            else controller.Move(true);
        }
    }
}