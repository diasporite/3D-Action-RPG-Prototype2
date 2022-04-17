using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class InputController : MonoBehaviour
    {
        public readonly Dictionary<Func<bool>, QueueAction> actions =
            new Dictionary<Func<bool>, QueueAction>();

        public virtual Vector2 InputMoveCharDir { get; }
        public virtual Vector3 MoveCharDir { get; }

        public virtual Vector2 InputMoveCamDir { get; }
        public virtual Vector3 MoveCamDir { get; }

        public virtual bool Defend() { return false; }
        public virtual bool Run() { return false; } 

        public virtual bool ToggleLock() { return false; }
        public virtual bool SelectNext() { return false; }
        public virtual bool SelectPrevious() { return false; }

        public virtual bool ActionL1() { return false; }
        public virtual bool ActionL2() { return false; }
        public virtual bool ActionR1() { return false; }
        public virtual bool ActionR2() { return false; }

        public virtual bool Char1() { return false; }
        public virtual bool Char2() { return false; }
        public virtual bool Char3() { return false; }
        public virtual bool Char4() { return false; }

        protected virtual void Init()
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
        }
    }
}