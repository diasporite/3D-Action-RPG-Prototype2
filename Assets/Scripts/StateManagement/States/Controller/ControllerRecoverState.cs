using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerRecoverState : IState
    {
        Controller controller;
        StateMachine csm;

        Movement movement;

        Health health;
        Stamina stamina;

        public ControllerRecoverState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            movement = controller.Movement;

            health = controller.Party.Health;
            stamina = controller.Party.Stamina;
        }

        public void Enter(params object[] args)
        {
            movement.State = MovementState.Walk;
            health.State = ResourceState.Recover;
            stamina.State = ResourceState.Recover;
        }

        public void ExecuteFrame()
        {
            stamina.Tick();

            if (stamina.Full)
                csm.ChangeState(StateID.ControllerMove);

            movement.MovePosition(controller.InputController.MoveCharXz, Time.deltaTime);
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