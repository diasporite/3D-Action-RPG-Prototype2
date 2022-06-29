using System.Collections;
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

            Model = GetComponent<CharacterModel>();
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
            sm.AddState(StateID.Empty, new EmptyState());

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
            CurrentState = sm.CurrentStateKey;

            sm.Update();

            // Test damage
            if (Input.GetKeyDown("space")) Combatant.OnDamage(new DamageInfo(Combatant, 35, 10));
            if (Input.GetKeyDown("tab")) Combatant.OnDamage(new DamageInfo(Combatant, 10, 35));
        }

        #region ControllerActions
        public void Move(bool strafe)
        {
            var dir = InputController.MoveChar;
            var ds = new Vector3(dir.x, 0, dir.y).normalized;

            if (strafe)
                Movement.MovePositionStrafe(ds, Time.deltaTime);
            else Movement.MovePositionFree(ds, Time.deltaTime);
        }

        public void Run()
        {
            var dir = InputController.MoveChar;
            var ds = new Vector3(dir.x, 0, dir.y).normalized;

            Movement.MovePositionRun(ds, Time.deltaTime);
        }

        public void Look(Vector2 dir)
        {

        }

        void Dpad(Vector2 dir)
        {

        }

        void Run(Vector2 dir)
        {
            if (sm.InState(StateID.ControllerMove) && dir != Vector2.zero)
                sm.ChangeState(StateID.ControllerRun);
        }

        void Walk(Vector2 dir)
        {
            if (sm.InState(StateID.ControllerRun) && dir != Vector2.zero)
                sm.ChangeState(StateID.ControllerMove);
        }

        void ToggleLock()
        {
            if (sm.InState(StateID.ControllerMove, StateID.ControllerStrafe, 
                StateID.ControllerRecover))
            {
                TargetSphere.Active = !TargetSphere.Active;

                if (TargetSphere.Active)
                {
                    TargetSphere.InvokeLockOn();

                    var targetFound = TargetSphere.SelectTargets();

                    if (targetFound)
                    {
                        if (sm.InState(StateID.ControllerMove))
                            sm.ChangeState(StateID.ControllerStrafe);
                    }
                    else
                    {
                        TargetSphere.Active = false;
                        sm.ChangeState(StateID.ControllerMove);
                    }
                }
                else
                {
                    TargetSphere.InvokeLockOff();

                    sm.ChangeState(StateID.ControllerMove);
                }
            }
        }

        void Roll()
        {
            print("roll");
            if (sm.InState(StateID.ControllerGuard)) sm.ChangeState(StateID.ControllerMove);
            else if (sm.InState(StateID.ControllerMove, StateID.ControllerRun, 
                StateID.ControllerStrafe, StateID.ControllerAction))
            {
                BattleAction action;

                if (TargetSphere.Active)
                    action = new BattleAction(this, new Vector3(InputController.MoveChar.x, 0, InputController.MoveChar.y),
                        Combatant.GetActionData(0), defendHash);
                else action = new BattleAction(this, Combatant.GetActionData(0), defendHash);

                ActionQueue.AddAction(action);
            }
        }

        void Guard()
        {
            print("guard");
            if (sm.InState(StateID.ControllerMove, StateID.ControllerRun, 
                StateID.ControllerStrafe))
                sm.ChangeState(StateID.ControllerGuard);
        }

        void GuardCancel()
        {
            if (sm.InState(StateID.ControllerGuard))
                sm.ChangeState(StateID.ControllerMove);
        }

        void Action(int index)
        {
            if (sm.InState(StateID.ControllerMove, StateID.ControllerRun,
                StateID.ControllerAction, StateID.ControllerStrafe))
            {
                index = Mathf.Clamp(index, 0, Combatant.Skillset.Count - 1);
                ActionQueue.AddAction(GetAttackAction(index));
            }
        }
        #endregion

        BattleAction GetAttackAction(int index)
        {
            return new BattleAction(this, Combatant.GetActionData(index + 1), attackHashes[index]);
        }

        // For use with stagger animation
        public void ReturnToMoveState()
        {
            sm.ChangeState(StateID.ControllerMove);
        }

        public void MoveTo(Vector3 pos)
        {
            Movement.MoveTo(pos);
        }

        public void Destroy()
        {
            Party.Party.Remove(this);

            Party.InvokeDeath();

            Destroy(gameObject);
        }
    }
}