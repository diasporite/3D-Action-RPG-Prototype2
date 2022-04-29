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
        [SerializeField] protected Vector2 moveChar;
        [SerializeField] protected Vector2 moveCam;
        [SerializeField] protected Vector2 dpad;

        [SerializeField] protected bool run;
        [SerializeField] protected bool defend;
        //[SerializeField] protected InputButton run = new InputButton(true);
        //[SerializeField] protected InputButton defend = new InputButton(false);

        [SerializeField] protected bool toggleLock;
        [SerializeField] protected bool selectNext;
        [SerializeField] protected bool selectPrevious;

        [SerializeField] protected bool actionL1;
        [SerializeField] protected bool actionL2;
        [SerializeField] protected bool actionR1;
        [SerializeField] protected bool actionR2;

        [SerializeField] protected bool char1;
        [SerializeField] protected bool char2;
        [SerializeField] protected bool char3;
        [SerializeField] protected bool char4;

        public readonly Dictionary<Func<bool>, QueueAction> actions =
            new Dictionary<Func<bool>, QueueAction>();

        public readonly Dictionary<ButtonID, bool> buttonDict = new Dictionary<ButtonID, bool>();

        public Vector2 MoveChar => moveChar;
        public Vector3 MoveCharXz => new Vector3(moveChar.x, 0, moveChar.y);

        public Vector2 MoveCam => moveCam;
        public Vector3 MoveCamXz => new Vector3(moveCam.x, 0, moveCam.y);

        public Vector2 Dpad => dpad;

        public bool Defend()
        {
            var pressed = defend;
            if (pressed) defend = false;
            return pressed;
        }

        public bool Run() => run;

        public virtual bool ToggleLock()
        {
            var pressed = toggleLock;
            if (pressed) toggleLock = false;
            return pressed;
        }

        public virtual bool SelectNext()
        {
            var pressed = selectNext;
            if (pressed) selectNext = false;
            return pressed;
        }

        public virtual bool SelectPrevious()
        {
            var pressed = selectPrevious;
            if (pressed) selectPrevious = false;
            return pressed;
        }

        public bool ActionL1()
        {
            var pressed = actionL1;
            if (pressed) actionL1 = false;
            return pressed;
        }

        public bool ActionL2()
        {
            var pressed = actionL2;
            if (pressed) actionL2 = false;
            return pressed;
        }

        public bool ActionR1()
        {
            var pressed = actionR1;
            if (pressed) actionR1 = false;
            return pressed;
        }

        public bool ActionR2()
        {
            var pressed = actionR2;
            if (pressed) actionR2 = false;
            return pressed;
        }

        public bool Char1() { return char1; }
        public bool Char2() { return char2; }
        public bool Char3() { return char3; }
        public bool Char4() { return char4; }

        public bool ButtonPressed(ButtonID id)
        {
            if (buttonDict.ContainsKey(id))
            {
                var pressed = buttonDict[id];
                if (pressed) buttonDict[id] = false;
                return pressed;
            }

            return false;
        }

        private void Awake()
        {
            Init();
        }

        protected void Init()
        {
            InitDict();
        }

        protected void InitDict()
        {
            actions.Add(ActionL1, QueueAction.ActionL1);
            actions.Add(ActionL2, QueueAction.ActionL2);
            actions.Add(ActionR1, QueueAction.ActionR1);
            actions.Add(ActionR2, QueueAction.ActionR2);
            actions.Add(Defend, QueueAction.Defend);
            actions.Add(Char1, QueueAction.Char1);
            actions.Add(Char2, QueueAction.Char2);
            actions.Add(Char3, QueueAction.Char3);
            actions.Add(Char4, QueueAction.Char4);

            buttonDict.Add(ButtonID.Defend, defend);
            buttonDict.Add(ButtonID.ToggleLock, toggleLock);
            buttonDict.Add(ButtonID.SelectNext, selectNext);
            buttonDict.Add(ButtonID.SelectPrevious, selectPrevious);
            buttonDict.Add(ButtonID.ActionL1, actionL1);
            buttonDict.Add(ButtonID.ActionL2, actionL2);
            buttonDict.Add(ButtonID.ActionR1, actionR1);
            buttonDict.Add(ButtonID.ActionR2, actionR2);
        }

        public void ResetDpad()
        {
            dpad = Vector2.zero;
        }
    }
}