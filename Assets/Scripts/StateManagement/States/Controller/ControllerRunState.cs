using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ControllerRunState : IState
    {
        Controller controller;
        StateMachine csm;

        Movement movement;

        Health health;
        Stamina stamina;
        InputController inputController;

        public ControllerRunState(Controller controller)
        {
            this.controller = controller;
            csm = controller.sm;

            movement = controller.Movement;

            health = controller.Party.Health;
            stamina = controller.Party.Stamina;
            inputController = controller.InputController;
        }

        public void Enter(params object[] args)
        {

        }

        public void ExecuteFrame()
        {
            health.Tick(0);
            stamina.Tick(-0.5f * Time.deltaTime);

            if (!movement.Grounded)
                csm.ChangeState(StateID.ControllerFall);
            else if (stamina.Empty)
                csm.ChangeState(StateID.ControllerMove);
            else if (controller.InputController.MoveChar == Vector2.zero)
                csm.ChangeState(StateID.ControllerMove);
            else controller.Run();
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