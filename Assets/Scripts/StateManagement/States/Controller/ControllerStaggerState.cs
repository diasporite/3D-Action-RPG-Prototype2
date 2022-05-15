using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerStaggerState : IState
    {
        Controller controller;
        StateMachine csm;

        float fadeTime = 0.1f;

        public ControllerStaggerState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;
        }

        public void Enter(params object[] args)
        {
            controller.TargetSphere.enabled = false;

            controller.Model.PlayAnimationFade(controller.staggerHash, 0, fadeTime);

            controller.ActionQueue.ClearActions();
        }

        public void ExecuteFrame()
        {
            controller.Party.Health.Tick(0);
            controller.Party.Stamina.Tick(0);
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