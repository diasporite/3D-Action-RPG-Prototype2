using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG_Project
{
    public enum ButtonID
    {
        None = 0,

        Run = 1,
        Defend = 2,

        ToggleLock = 5,
        SelectNext = 6,
        SelectPrevious = 7,

        ActionL1 = 10,
        ActionL2 = 11,
        ActionR1 = 12,
        ActionR2 = 13,
    }

    public class InputController : MonoBehaviour
    {
        public event Action<Vector2> DpadAction;

        public event Action<Vector2> RunAction;
        public event Action<Vector2> WalkAction;

        public event Action LockAction;
        public event Action LockOnAction;
        public event Action LockOffAction;

        public event Action DashAction;
        public event Action GuardAction;
        public event Action GuardCancel;

        // Action5-8 are for enemies
        public event Action<int> OnAction;

        PartyController party;

        [field: SerializeField] public Vector2 MoveChar { get; protected set; }
        [field: SerializeField] public Vector2 MoveCam { get; protected set; }
        [field: SerializeField] public Vector2 Dpad { get; protected set; }

        [field: SerializeField] public bool Run { get; protected set; }
        [field: SerializeField] public bool Defend { get; protected set; }

        public Controller Controller => party.CurrentController;

        //public bool Defend()
        //{
        //    var pressed = defend;
        //    if (pressed) defend = false;
        //    return pressed;
        //}

        //public bool Run() => run;

        //public virtual bool ToggleLock()
        //{
        //    var pressed = toggleLock;
        //    if (pressed) toggleLock = false;
        //    return pressed;
        //}

        //public virtual bool SelectNext()
        //{
        //    var pressed = selectNext;
        //    if (pressed) selectNext = false;
        //    return pressed;
        //}

        //public virtual bool SelectPrevious()
        //{
        //    var pressed = selectPrevious;
        //    if (pressed) selectPrevious = false;
        //    return pressed;
        //}

        //public bool ActionL1()
        //{
        //    var pressed = actionL1;
        //    if (pressed) actionL1 = false;
        //    return pressed;
        //}

        //public bool ActionL2()
        //{
        //    var pressed = actionL2;
        //    if (pressed) actionL2 = false;
        //    return pressed;
        //}

        //public bool ActionR1()
        //{
        //    var pressed = actionR1;
        //    if (pressed) actionR1 = false;
        //    return pressed;
        //}

        //public bool ActionR2()
        //{
        //    var pressed = actionR2;
        //    if (pressed) actionR2 = false;
        //    return pressed;
        //}

        //public bool Char1() { return char1; }
        //public bool Char2() { return char2; }
        //public bool Char3() { return char3; }
        //public bool Char4() { return char4; }

        private void Awake()
        {
            Init();
        }

        protected void Init()
        {
            party = GetComponent<PartyController>();
        }

        public void ResetDpad()
        {
            Dpad = Vector2.zero;
        }

        #region InvokeMethods
        protected void InvokeDpad(Vector2 dir)
        {
            DpadAction?.Invoke(dir);
        }

        protected void InvokeRun(Vector2 dir)
        {
            RunAction?.Invoke(dir);
        }

        protected void InvokeWalk(Vector2 dir)
        {
            WalkAction?.Invoke(dir);
        }

        protected void InvokeLock()
        {
            LockAction?.Invoke();
        }

        protected void InvokeLockOn()
        {
            LockOnAction?.Invoke();
        }

        protected void InvokeLockOff()
        {
            LockOffAction?.Invoke();
        }

        protected void InvokeRoll()
        {
            DashAction?.Invoke();
        }

        protected void InvokeGuard()
        {
            GuardAction?.Invoke();
        }

        protected void InvokeAction(int index)
        {
            OnAction?.Invoke(index);
        }
        #endregion
    }
}