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
        InputController inputController;
        ActionQueue actionQueue;

        Dictionary<Func<bool>, QueueAction> actions;

        public ControllerMoveState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            movement = controller.Movement;
            inputController = controller.InputController;
            actionQueue = controller.Party.ActionQueue;

            actions = new Dictionary<Func<bool>, QueueAction>();
            Func<bool> l1 = inputController.ActionL1;
            actions.Add(l1, QueueAction.ActionL1);
            actions.Add(inputController.ActionL2, QueueAction.ActionL2);
            actions.Add(inputController.ActionR1, QueueAction.ActionR1);
            actions.Add(inputController.ActionR2, QueueAction.ActionR2);
            actions.Add(inputController.Defend, QueueAction.Defend);
            actions.Add(inputController.Char1, QueueAction.Char1);
            actions.Add(inputController.Char2, QueueAction.Char2);
            actions.Add(inputController.Char3, QueueAction.Char3);
            actions.Add(inputController.Char4, QueueAction.Char4);
        }

        public void Enter(params object[] args)
        {
            movement.State = MovementState.Walk;
        }

        public void ExecuteFrame()
        {
            if (inputController.Run()) csm.ChangeState(StateID.ControllerRun);
            else if (Action()) csm.ChangeState(StateID.ControllerAction);

            movement.MovePosition(inputController.MoveCharDir, Time.deltaTime);
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
            foreach(var inp in actions.Keys)
            {
                if (inp.Invoke())
                {
                    actionQueue.AddAction(actions[inp]);
                    return true;
                }
            }
            return false;
        }
    }
}