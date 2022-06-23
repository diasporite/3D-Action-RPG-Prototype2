﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerStaggerState : IState
    {
        Controller controller;
        StateMachine csm;

        float animNormTime;

        public ControllerStaggerState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;
        }

        public void Enter(params object[] args)
        {
            controller.TargetSphere.Active = false;

            controller.Model.PlayAnimationFade(controller.staggerHash, 0);

            controller.ActionQueue.ClearActions();
        }

        public void ExecuteFrame()
        {
            controller.Party.Health.Tick(0);
            controller.Party.Stamina.Tick(0);

            animNormTime = controller.Model.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animNormTime >= 1f) csm.ChangeState(StateID.ControllerMove);
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