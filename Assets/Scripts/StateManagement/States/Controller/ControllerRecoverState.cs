﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerRecoverState : IState
    {
        Controller controller;
        StateMachine csm;

        Movement movement;

        Health health;
        Stamina stamina;

        float fadeTime = 0.1f;

        public ControllerRecoverState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            movement = controller.Movement;

            health = controller.Party.Health;
            stamina = controller.Party.Stamina;
        }

        public void Enter(params object[] args)
        {
            movement.State = MovementState.Walk;
            health.State = ResourceState.Recover;
            stamina.State = ResourceState.Recover;

            if (controller.TargetSphere.Active)
                controller.Model.PlayAnimationFade(controller.strafeHash, 0, fadeTime);
            else controller.Model.PlayAnimationFade(controller.moveHash, 0, fadeTime);
        }

        public void ExecuteFrame()
        {
            health.Tick(0);
            stamina.Tick();

            if (stamina.Full)
            {
                if (controller.TargetSphere.Active)
                    csm.ChangeState(StateID.ControllerStrafe);
                else csm.ChangeState(StateID.ControllerMove);

                return;
            }

            if (!movement.Grounded)
                csm.ChangeState(StateID.ControllerFall);
            else controller.Move(controller.TargetSphere.Active);
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