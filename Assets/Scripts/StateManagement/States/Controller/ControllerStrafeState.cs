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
            controller.TargetSphere.enabled = true;

            if (controller.TargetSphere.NoTargets)
            {
                csm.ChangeState(StateID.ControllerMove);
                return;
            }

            movement.State = MovementState.Walk;
            health.State = ResourceState.Regen;
            stamina.State = ResourceState.Regen;

            controller.Model.PlayAnimationFade(controller.strafeHash, 0);
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

            if (controller.TargetSphere.NoTargets) csm.ChangeState(StateID.ControllerMove);
            else
            {
                //var dir = inputController.MoveCharXz;
                //controller.MoveStrafe(dir);
                //controller.Model.SetAnimDir(dir);
            }
        }
    }
}