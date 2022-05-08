using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Controller : MonoBehaviour
    {
        public readonly int moveHash = Animator.StringToHash("Move");
        public readonly int strafeHash = Animator.StringToHash("Strafe");
        public readonly int fallHash = Animator.StringToHash("Fall");

        public readonly int defendHash = Animator.StringToHash("Defend");
        public readonly int strafeDefendHash = Animator.StringToHash("StrafeDefend");

        public readonly int actionL1Hash = Animator.StringToHash("ActionL1");
        public readonly int actionL2Hash = Animator.StringToHash("ActionL2");
        public readonly int actionR1Hash = Animator.StringToHash("ActionR1");
        public readonly int actionR2Hash = Animator.StringToHash("ActionR2");

        [SerializeField] StateID currentState;
        [SerializeField] bool actionMovement;

        PartyController party;
        InputController inputController;
        ActionQueue actionQueue;

        Movement movement;
        Combatant combatant;
        CharacterModel model;

        TargetSphere targetSphere;
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
        public TargetSphere TargetSphere => targetSphere;
        public CameraPivot Pivot => pivot;

        public void Init()
        {
            party = GetComponentInParent<PartyController>();
            inputController = party.InputController;
            actionQueue = party.ActionQueue;

            movement = GetComponent<Movement>();
            combatant = GetComponent<Combatant>();
            model = GetComponent<CharacterModel>();

            targetSphere = GetComponentInChildren<TargetSphere>();

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

        public void Move()
        {
            movement.MovePosition(inputController.MoveCharXz, Time.deltaTime);
        }

        public void Move(Vector3 ds)
        {
            movement.MovePosition(ds, Time.deltaTime);
        }

        public void Switch()
        {
            var dpad = inputController.Dpad;

            if (dpad == Vector2.up) party.SwitchController(0);
            else if (dpad == Vector2.left) party.SwitchController(1);
            else if (dpad == Vector2.right) party.SwitchController(2);
            else if (dpad == Vector2.down) party.SwitchController(3);

            inputController.ResetDpad();
        }

        public BattleAction GetAction(QueueAction action)
        {
            switch (action)
            {
                case QueueAction.ActionL1:
                    return new BattleAction(this, "ActionL1", actionL1Hash);
                case QueueAction.ActionL2:
                    return new BattleAction(this, "ActionL2", actionL2Hash);
                case QueueAction.ActionR1:
                    return new BattleAction(this, "ActionR1", actionR1Hash);
                case QueueAction.ActionR2:
                    return new BattleAction(this, "ActionR2", actionR2Hash);

                case QueueAction.Char1:
                    return new BattleAction(this, "Char1");
                case QueueAction.Char2:
                    return new BattleAction(this, "Char2");
                case QueueAction.Char3:
                    return new BattleAction(this, "Char3");
                case QueueAction.Char4:
                    return new BattleAction(this, "Char4");

                case QueueAction.Defend:
                    if (targetSphere.enabled)
                        return new BattleAction(this, "Defend", strafeDefendHash);
                    return new BattleAction(this, "Defend", defendHash);

                default: return null;
            }
        }

        public void AddAction(QueueAction action)
        {
            actionQueue.AddAction(GetAction(action));
        }
    }
}