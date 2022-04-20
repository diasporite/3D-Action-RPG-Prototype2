using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{


    public class Controller : MonoBehaviour
    {
        [SerializeField] StateID currentState;
        [SerializeField] bool actionMovement;

        PartyController party;
        InputController inputController;
        ActionQueue actionQueue;

        Movement movement;
        Combatant combatant;

        CharacterModel model;
        CameraPivot pivot;

        public readonly StateMachine sm = new StateMachine();

        public StateID CurrentState => currentState;
        public bool ActionMovement => actionMovement;

        public PartyController Party => party;
        public InputController InputController => inputController;
        public ActionQueue ActionQueue => actionQueue;

        public Movement Movement => movement;
        public Combatant Combatant => combatant;

        public CharacterModel Model => model;
        public CameraPivot Pivot => pivot;

        public void Init()
        {
            party = GetComponentInParent<PartyController>();
            inputController = party.InputController;
            actionQueue = party.ActionQueue;

            movement = GetComponent<Movement>();
            combatant = GetComponent<Combatant>();

            model = GetComponentInChildren<CharacterModel>();

            pivot = party.Pivot;

            movement.Init();
            combatant.Init(0);

            InitSM();
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
            sm.AddState(StateID.ControllerStrafe, new ControllerStrafeState(this));
        }

        public void UpdateController()
        {
            currentState = (StateID)sm.GetCurrentKey;

            sm.Update();
        }

        public void AdvanceAction()
        {
            actionQueue.AdvanceAction();
        }

        public void LockActionMovement()
        {
            actionMovement = false;
        }

        public void UnlockActionMovement()
        {
            actionMovement = true;
        }
    }
}