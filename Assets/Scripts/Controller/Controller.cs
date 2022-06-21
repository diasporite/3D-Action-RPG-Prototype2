﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum ControllerType
    {
        Player = 0,
        Enemy = 1,
        Boss = 2,
    }

    public class Controller : MonoBehaviour
    {
        // 1 = L1
        // 2 = L2
        // 3 = R1
        // 4 = R2

        #region AnimatorHashes
        public readonly int moveHash = Animator.StringToHash("Move");
        public readonly int strafeHash = Animator.StringToHash("Strafe");
        public readonly int fallHash = Animator.StringToHash("Fall");

        public readonly int guardHash = Animator.StringToHash("Guard");
        public readonly int defendHash = Animator.StringToHash("Defend");
        public readonly int strafeDefendHash = Animator.StringToHash("StrafeDefend");

        public readonly int staggerHash = Animator.StringToHash("Stagger");
        public readonly int deathHash = Animator.StringToHash("Death");

        int[] attackHashes = new int[] { Animator.StringToHash("Action1"),
            Animator.StringToHash("Action2"), Animator.StringToHash("Action3"),
            Animator.StringToHash("Action4"), Animator.StringToHash("Action5"),
            Animator.StringToHash("Action6"), Animator.StringToHash("Action7"),
            Animator.StringToHash("Action8") };
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

        private void Awake()
        {
            Party = GetComponentInParent<PartyController>();

            Movement = GetComponent<Movement>();
            Combatant = GetComponent<Combatant>();

            Model = GetComponentInChildren<CharacterModel>();
        }

        private void OnEnable()
        {
            if (InputController != null) SubscribeToDelegates();
        }

        private void OnDisable()
        {
            UnsubscribeFromDelegates();
        }

        public void Init(bool isPlayer)
        {
            IsPlayer = isPlayer;

            InputController = Party.InputController;
            ActionQueue = Party.ActionQueue;
            TargetSphere = Party.TargetSphere;

            Movement.Init();
            Combatant.Init(1);
            Model.Init();

            SubscribeToDelegates();

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
            sm.AddState(StateID.ControllerGuard, new ControllerGuardState(this));
        }

        void SubscribeToDelegates()
        {
            InputController.DpadAction += Dpad;

            InputController.RunAction += Run;
            InputController.WalkAction += Walk;

            InputController.LockAction += ToggleLock;

            InputController.RollAction += Roll;
            InputController.GuardAction += Guard;
            InputController.OnAction += Action;
        }

        void UnsubscribeFromDelegates()
        {
            InputController.DpadAction -= Dpad;

            InputController.RunAction -= Run;
            InputController.WalkAction -= Walk;

            InputController.LockAction -= ToggleLock;

            InputController.RollAction -= Roll;
            InputController.GuardAction -= Guard;
            InputController.OnAction -= Action;
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

        #region ControllerActions
        public void Move()
        {
            var dir = InputController.MoveChar;
            var ds = new Vector3(dir.x, 0, dir.y).normalized;

            if (TargetSphere.enabled)
                Movement.MovePositionStrafe(ds, Time.deltaTime, false);
            else Movement.MovePositionFree(ds, Time.deltaTime, false);
        }

        public void Look(Vector2 dir)
        {

        }

        void Dpad(Vector2 dir)
        {

        }

        void Run(Vector2 dir)
        {
            if (dir != Vector2.zero) sm.ChangeState(StateID.ControllerRun);
        }

        void Walk(Vector2 dir)
        {
            if (dir != Vector2.zero) sm.ChangeState(StateID.ControllerMove);
        }

        void ToggleLock()
        {
            if (CurrentState == StateID.ControllerMove)
                sm.ChangeState(StateID.ControllerStrafe);
            else if (CurrentState == StateID.ControllerStrafe)
                sm.ChangeState(StateID.ControllerMove);
        }

        void Roll()
        {
            if (CurrentState == StateID.ControllerGuard) sm.ChangeState(StateID.ControllerMove);
            else
            {
                BattleAction action;

                if (TargetSphere.enabled)
                    action = new BattleAction(this, new Vector3(InputController.MoveChar.x, 0, InputController.MoveChar.y),
                        Combatant.GetActionData(0), defendHash);
                else action = new BattleAction(this, Combatant.GetActionData(0), defendHash);

                ActionQueue.AddAction(action);
            }
        }

        void Guard()
        {
            sm.ChangeState(StateID.ControllerGuard);
        }

        void Action(int index)
        {
            index = Mathf.Clamp(index, 0, Combatant.Skillset.Count - 1);
            ActionQueue.AddAction(GetAttackAction(index));
        }
        #endregion

        #region Movement
        public void MoveFree(Vector3 ds)
        {
            Movement.MovePositionFree(ds, Time.deltaTime, false);
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

        BattleAction GetAttackAction(int index)
        {
            return new BattleAction(this, Combatant.GetActionData(index + 1), attackHashes[index]);
        }

        public BattleAction GetAction(QueueAction action)
        {
            switch (action)
            {
                //case QueueAction.ActionL1:
                //    return new BattleAction(this, Combatant.GetActionData(1), actionL1Hash);
                //case QueueAction.ActionL2:
                //    return new BattleAction(this, Combatant.GetActionData(2), actionL2Hash);
                //case QueueAction.ActionR1:
                //    return new BattleAction(this, Combatant.GetActionData(3), actionR1Hash);
                //case QueueAction.ActionR2:
                //    return new BattleAction(this, Combatant.GetActionData(4), actionR2Hash);

                //case QueueAction.Char1:
                //    return new BattleAction(this, "Char1");
                //case QueueAction.Char2:
                //    return new BattleAction(this, "Char2");
                //case QueueAction.Char3:
                //    return new BattleAction(this, "Char3");
                //case QueueAction.Char4:
                //    return new BattleAction(this, "Char4");

                //case QueueAction.Defend:
                //    if (TargetSphere.enabled)
                //        return new BattleAction(this, InputController.MoveCharXz, 
                //            Combatant.GetActionData(0), defendHash);
                //    return new BattleAction(this, Combatant.GetActionData(0), defendHash);

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