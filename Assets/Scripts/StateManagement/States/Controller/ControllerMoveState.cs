using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerMoveState : IState
    {
        Controller controller;
        StateMachine csm;

        Movement movement;

        Health health;
        Stamina stamina;
        InputController inputController;
        ActionQueue actionQueue;

        Vector3 ds;

        public ControllerMoveState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            movement = controller.Movement;

            health = controller.Party.Health;
            stamina = controller.Party.Stamina;
            inputController = controller.InputController;
            actionQueue = controller.Party.ActionQueue;
        }

        public void Enter(params object[] args)
        {
            controller.Model.PlayAnimationFade(controller.moveHash, 0, true);

            actionQueue.ClearActions();
        }

        public void ExecuteFrame()
        {
            ds = controller.InputController.MoveChar;

            if (ds != Vector3.zero) health.Tick();
            else health.Tick(0);

            stamina.Tick();

            if (!movement.Grounded)
                csm.ChangeState(StateID.ControllerFall);
            //else if (stamina.Empty)
            //    csm.ChangeState(StateID.ControllerRecover);
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