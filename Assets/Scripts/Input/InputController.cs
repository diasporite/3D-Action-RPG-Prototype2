using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class InputController : MonoBehaviour
    {
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
    }
}