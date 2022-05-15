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

        float fadeTime = 0.1f;

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
            movement.State = MovementState.Walk;
            health.State = ResourceState.Regen;
            stamina.State = ResourceState.Regen;

            controller.TargetSphere.enabled = false;

            controller.Model.PlayAnimationFade(controller.moveHash, 0, fadeTime);

            actionQueue.ClearActions();
        }

        public void ExecuteFrame()
        {
            ds = inputController.MoveCharXz;

            stamina.Tick();

            if (stamina.Empty)
                csm.ChangeState(StateID.ControllerRecover);
            else
            {
                if (inputController.Run()) csm.ChangeState(StateID.ControllerRun);
                //if (inputController.Run())
                //{
                //    if (ds != Vector3.zero)
                //        csm.ChangeState(StateID.ControllerRun);
                //}
                else if (inputController.ToggleLock()) csm.ChangeState(StateID.ControllerStrafe);
                else if (Action()) csm.ChangeState(StateID.ControllerAction);
                else if (inputController.Dpad != Vector2.zero) controller.Switch();
                else
                {
                    controller.MoveFree(ds);

                    if (ds != Vector3.zero) health.Tick();
                    else health.Tick(0);
                }
            }
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

        bool Action()
        {
            foreach(var inp in inputController.actions.Keys)
            {
                if (inp.Invoke())
                {
                    controller.AddAction(inputController.actions[inp]);
                    return true;
                }
            }
            return false;
        }
    }
}