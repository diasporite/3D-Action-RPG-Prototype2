using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{


    public class Controller : MonoBehaviour
    {
        PartyController party;
        InputController inputController;
        ActionQueue actionQueue;

        Movement movement;

        CharacterModel model;
        CameraPivot pivot;

        public readonly StateMachine sm = new StateMachine();

        public PartyController Party => party;

        public Movement Movement => movement;
        public InputController InputController => inputController;

        public CharacterModel Model => model;
        public CameraPivot Pivot => pivot;

        public void Init()
        {
            party = GetComponentInParent<PartyController>();
            inputController = party.InputController;
            actionQueue = party.ActionQueue;

            movement = GetComponent<Movement>();

            model = GetComponentInChildren<CharacterModel>();

            pivot = party.Pivot;

            movement.Init();
            
            InitSM();

            sm.ChangeState(StateID.ControllerMove);
        }

        void InitSM()
        {
            sm.AddState(StateID.ControllerMove, new ControllerMoveState(this));
            sm.AddState(StateID.ControllerRun, new ControllerRunState(this));
            sm.AddState(StateID.ControllerFall, new ControllerFallState(this));
            sm.AddState(StateID.ControllerRecover, new ControllerRecoverState(this));
            sm.AddState(StateID.ControllerAction, new ControllerActionState(this));
            sm.AddState(StateID.ControllerStagger, new ControllerStaggerState(this));
            sm.AddState(StateID.ControllerDeath, new ControllerDeathState(this));
        }

        public void UpdateController()
        {
            sm.Update();
        }

        public void AdvanceAction()
        {

        }
    }
}