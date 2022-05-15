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

        [SerializeField] bool isPlayer;

        [SerializeField] StateID currentState;
        [SerializeField] bool actionMovement;

        PartyController party;
        InputController inputController;
        ActionQueue actionQueue;

        Movement movement;
        Combatant combatant;
        CharacterModel model;

        CameraPivot pivot;
        TargetSphere targetSphere;

        public readonly StateMachine sm = new StateMachine();

        public bool IsPlayer => isPlayer;

        public StateID CurrentState => currentState;
        public bool ActionMovement => actionMovement;

        public PartyController Party => party;
        public InputController InputController => inputController;
        public ActionQueue ActionQueue => actionQueue;

        public Movement Movement => movement;
        public Combatant Combatant => combatant;
        public CharacterModel Model => model;

        public CameraPivot Pivot => pivot;
        public TargetSphere TargetSphere => targetSphere;

        public void Init(bool isPlayer)
        {
            this.isPlayer = isPlayer;

            party = GetComponentInParent<PartyController>();
            inputController = party.InputController;
            actionQueue = party.ActionQueue;

            movement = GetComponent<Movement>();
            combatant = GetComponent<Combatant>();
            model = GetComponent<CharacterModel>();

            pivot = party.Pivot;
            targetSphere = party.TargetSphere;

            movement.Init();
            combatant.Init(0);
            model.Init();

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

            // Test damage
            if (Input.GetKeyDown("space")) combatant.OnDamage(new DamageInfo(combatant, 35, 10));
        }

        public void AdvanceAction()
        {
            actionQueue.AdvanceAction();
        }

        #region Movement
        public void LockActionMovement()
        {
            actionMovement = false;
        }

        public void UnlockActionMovement()
        {
            actionMovement = true;
        }

        public void MoveFree()
        {
            movement.MovePositionFree(inputController.MoveCharXz, Time.deltaTime);
        }

        public void MoveFree(Vector3 ds)
        {
            movement.MovePositionFree(ds, Time.deltaTime);
        }

        public void MoveStrafe()
        {
            movement.MovePositionStrafe(inputController.MoveCharXz, Time.deltaTime);
        }

        public void MoveStrafe(Vector3 ds)
        {
            movement.MovePositionStrafe(ds, Time.deltaTime);
        }
        #endregion

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
                    return new BattleAction(this, "ActionL1", actionL1Hash, 20);
                case QueueAction.ActionL2:
                    return new BattleAction(this, "ActionL2", actionL2Hash, 20);
                case QueueAction.ActionR1:
                    return new BattleAction(this, "ActionR1", actionR1Hash, 20);
                case QueueAction.ActionR2:
                    return new BattleAction(this, "ActionR2", actionR2Hash, 20);

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
                        return new BattleAction(this, "Defend", strafeDefendHash, 20);
                    return new BattleAction(this, "Defend", defendHash, 20);

                default: return null;
            }
        }

        public void AddAction(QueueAction action)
        {
            actionQueue.AddAction(GetAction(action));
        }

        // For use with stagger and death animations
        public void ReturnToMoveState()
        {
            sm.ChangeState(StateID.ControllerMove);
        }
    }
}