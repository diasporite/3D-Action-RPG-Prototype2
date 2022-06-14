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
            if (controller.TargetSphere.NoTargets)
            {
                csm.ChangeState(StateID.ControllerMove);
                return;
            }

            controller.Pivot.ToggleLock(true);

            movement.State = MovementState.Walk;
            health.State = ResourceState.Regen;
            stamina.State = ResourceState.Regen;

            controller.Model.PlayAnimationFade("Strafe", 0, 0.1f);
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

            //if (inputController.ToggleLock()) csm.ChangeState(StateID.ControllerMove);
            if (controller.TargetSphere.NoTargets) csm.ChangeState(StateID.ControllerMove);
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

                var dir = inputController.MoveCharXz;
                controller.MoveStrafe(dir);
                controller.Model.SetAnimDir(dir);
            }
        }
    }
}