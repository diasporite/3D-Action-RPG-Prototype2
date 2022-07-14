using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerDeathState : IState
    {
        Controller controller;
        StateMachine csm;

        Animator anim;
        float animNormTime;

        public ControllerDeathState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            anim = controller.Model.Anim;
        }

        public void Enter(params object[] args)
        {
            controller.Model.PlayAnimationFade(controller.deathHash, 0, false);

            controller.ActionQueue.ClearActions();

            animNormTime = 0;

            controller.GetComponentInParent<CharacterController>().enabled = false;
            controller.GetComponent<Collider>().enabled = false;
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