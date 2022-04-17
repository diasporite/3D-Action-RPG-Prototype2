﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerActionState : IState
    {
        Controller controller;
        StateMachine csm;

        CharacterModel model;

        InputController inputController;
        ActionQueue actionQueue;

        public ControllerActionState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            model = controller.Model;

            inputController = controller.Party.InputController;
            actionQueue = controller.Party.ActionQueue;
        }

        public void Enter(params object[] args)
        {
            model.PlayAnimation("Action1", 0);
        }

        public void ExecuteFrame()
        {
            foreach (var inp in inputController.actions.Keys)
                if (inp.Invoke())
                    actionQueue.AddAction(inputController.actions[inp]);
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