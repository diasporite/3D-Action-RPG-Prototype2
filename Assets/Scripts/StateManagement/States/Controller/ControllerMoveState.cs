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

            controller.Pivot.ToggleLock(false);

            controller.Model.PlayAnimationFade("Move", 0, 0.1f);
        }

        public void ExecuteFrame()
        {
            if (stamina.Empty)
                csm.ChangeState(StateID.ControllerRecover);
            else
            {
                if (inputController.Run()) csm.ChangeState(StateID.ControllerRun);
                else if (inputController.ToggleLock()) csm.ChangeState(StateID.ControllerStrafe);
                else if (Action()) csm.ChangeState(StateID.ControllerAction);
                else
                {
                    var ds = inputController.MoveCharXz;

                    movement.MovePosition(ds, Time.deltaTime);

                    if (ds != Vector3.zero) health.Tick();
                    stamina.Tick();
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
                    actionQueue.AddAction(inputController.actions[inp]);
                    return true;
                }
            }
            return false;
        }
    }
}