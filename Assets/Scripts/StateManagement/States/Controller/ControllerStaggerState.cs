﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerStaggerState : IState
    {
        Controller controller;
        StateMachine csm;

        int actionIndex;

        public ControllerStaggerState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;
        }

        public void Enter(params object[] args)
        {

        }

        public void ExecuteFrame()
        {

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