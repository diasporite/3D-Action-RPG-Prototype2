using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerDeathState : IState
    {
        Controller controller;
        StateMachine csm;

        float fadeTime = 0.1f;

        Animator anim;

        public ControllerDeathState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            anim = controller.Model.Anim;
        }

        public void Enter(params object[] args)
        {
            controller.TargetSphere.Active = false;

            controller.Model.PlayAnimationFade(controller.deathHash, 0, fadeTime);

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