﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerGuardState : IState
    {
        Controller controller;
        StateMachine csm;

        public ControllerGuardState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {

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
        #endregion

        void Command()
        {
            if (controller.Party.Stamina.Empty)
                csm.ChangeState(StateID.ControllerRecover);
        }
    }
}