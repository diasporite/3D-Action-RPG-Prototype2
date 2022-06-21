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

        float fadeTime = 0.25f;

        public ControllerFallState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            movement = controller.Movement;
        }

        public void Enter(params object[] args)
        {
            controller.Model.PlayAnimationFade(controller.fallHash, 0, fadeTime);
        }

        public void ExecuteFrame()
        {
            movement.State = MovementState.Fall;

            controller.Party.Health.Tick(0);
            controller.Party.Stamina.Tick(0);

            if (movement.Grounded)
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
            //controller.Model.SetAnimFalling(false);
        }
    }
}