using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Controller : MonoBehaviour
    {
        #region AnimatorHashes
        public readonly int moveHash = Animator.StringToHash("Move");
        public readonly int strafeHash = Animator.StringToHash("Strafe");
        public readonly int fallHash = Animator.StringToHash("Fall");

        public readonly int defendHash = Animator.StringToHash("Defend");
        public readonly int strafeDefendHash = Animator.StringToHash("StrafeDefend");

        public readonly int staggerHash = Animator.StringToHash("Stagger");
        public readonly int deathHash = Animator.StringToHash("Death");

        public readonly int actionL1Hash = Animator.StringToHash("ActionL1");
        public readonly int actionL2Hash = Animator.StringToHash("ActionL2");
        public readonly int actionR1Hash = Animator.StringToHash("ActionR1");
        public readonly int actionR2Hash = Animator.StringToHash("ActionR2");
        #endregion

        [field: SerializeField] public bool IsPlayer { get; private set; }

        [field: SerializeField] public StateID CurrentState { get; private set; }

        public PartyController Party { get; private set; }
        public InputController InputController { get; private set; }
        public ActionQueue ActionQueue { get; private set; }

        public Movement Movement { get; private set; }
        public Combatant Combatant { get; private set; }
        public CharacterModel Model { get; private set; }

        public TargetSphere TargetSphere { get; private set; }

        public readonly StateMachine sm = new StateMachine();

        public void Init(bool isPlayer)
        {
            IsPlayer = isPlayer;

            Party = GetComponentInParent<PartyController>();
            InputController = Party.InputController;
            ActionQueue = Party.ActionQueue;

            Movement = GetComponent<Movement>();
            Combatant = GetComponent<Combatant>();

            Model = GetComponentInChildren<CharacterModel>();

            TargetSphere = Party.TargetSphere;

            Movement.Init();
            Combatant.Init(0);
            Model.Init();

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
            sm.AddState(StateID.controllerGuard, new ControllerGuardState(this));
        }

        public void UpdateController()
        {
            CurrentState = (StateID)sm.GetCurrentKey;

            sm.Update();

            // Test damage
            if (Input.GetKeyDown("space")) Combatant.OnDamage(new DamageInfo(Combatant, 35, 10));
        }

        public void AdvanceAction()
        {
            ActionQueue.AdvanceAction();
        }

        #region Movement
        public void MoveFree()
        {
            Movement.MovePositionFree(InputController.MoveCharXz, Time.deltaTime, false);
        }

        public void MoveFree(Vector3 ds)
        {
            Movement.MovePositionFree(ds, Time.deltaTime, false);
        }

        public void MoveStrafe()
        {
            Movement.MovePositionStrafe(InputController.MoveCharXz, Time.deltaTime, false);
        }

        public void MoveStrafe(Vector3 ds)
        {
            Movement.MovePositionStrafe(ds, Time.deltaTime, false);
        }
        #endregion

        public void Switch()
        {
            var dpad = InputController.Dpad;

            if (dpad == Vector2.up) Party.SwitchController(0);
            else if (dpad == Vector2.left) Party.SwitchController(1);
            else if (dpad == Vector2.right) Party.SwitchController(2);
            else if (dpad == Vector2.down) Party.SwitchController(3);

            InputController.ResetDpad();
        }

        public BattleAction GetAction(QueueAction action)
        {
            switch (action)
            {
                case QueueAction.ActionL1:
                    return new BattleAction(this, Combatant.GetActionData(1), actionL1Hash);
                case QueueAction.ActionL2:
                    return new BattleAction(this, Combatant.GetActionData(2), actionL2Hash);
                case QueueAction.ActionR1:
                    return new BattleAction(this, Combatant.GetActionData(3), actionR1Hash);
                case QueueAction.ActionR2:
                    return new BattleAction(this, Combatant.GetActionData(4), actionR2Hash);

                //case QueueAction.Char1:
                //    return new BattleAction(this, "Char1");
                //case QueueAction.Char2:
                //    return new BattleAction(this, "Char2");
                //case QueueAction.Char3:
                //    return new BattleAction(this, "Char3");
                //case QueueAction.Char4:
                //    return new BattleAction(this, "Char4");

                case QueueAction.Defend:
                    if (TargetSphere.enabled)
                        return new BattleAction(this, InputController.MoveCharXz, 
                            Combatant.GetActionData(0), defendHash);
                    return new BattleAction(this, Combatant.GetActionData(0), defendHash);

                default: return null;
            }
        }

        public void AddAction(QueueAction action)
        {
            ActionQueue.AddAction(GetAction(action));
        }

        // For use with stagger and death animations
        public void ReturnToMoveState()
        {
            sm.ChangeState(StateID.ControllerMove);
        }
    }
}