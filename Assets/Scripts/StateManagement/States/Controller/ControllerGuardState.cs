using System.Collections;
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
            controller.Model.PlayAnimationFade(controller.guardHash, 0);
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
            controller.Party.Health.Tick(0);
            controller.Party.Stamina.Tick(0);

            if (controller.Party.Stamina.Empty)
                csm.ChangeState(StateID.ControllerRecover);
        }
    }
}