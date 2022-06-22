using System.Collections;
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

        AnimationData animData;
        float animNormTime;
        float speed;

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
            actionQueue.StopChain();
        }

        void Command()
        {
            controller.Party.Health.Tick(0);
            controller.Party.Stamina.Tick(0);

            if (!actionQueue.Executing)
            {
                if (!controller.Movement.Grounded)
                    csm.ChangeState(StateID.ControllerFall);
                else if (controller.Party.Stamina.Empty)
                    csm.ChangeState(StateID.ControllerRecover);
                else if (controller.TargetSphere.enabled)
                    csm.ChangeState(StateID.ControllerStrafe);
                else csm.ChangeState(StateID.ControllerMove);
            }
            else
            {
                animData = actionQueue.TopAction.Action.Animation;

                if (animData.motion.Length > 0)
                {
                    animNormTime = 0f;
                    animNormTime = controller.Model.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    animNormTime -= Mathf.Floor(animNormTime);  // % operator doesn't work on floats
                    Debug.Log(animNormTime);
                    speed = animData.CurrentMoveData(animNormTime).ForwardSpeed;

                    //controller.Movement.MovePositionFree(speed, controller.transform.forward, Time.deltaTime, false);
                    controller.Movement.MovePositionForward(speed, Time.deltaTime, false);
                }
            }
        }
    }
}