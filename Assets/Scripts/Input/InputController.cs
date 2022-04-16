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

        public virtual bool Defend { get; }
        public virtual bool Run { get; } 

        public virtual bool ToggleLock { get; }
        public virtual bool SelectNext { get; }
        public virtual bool SelectPrevious { get; }

        public virtual bool ActionL1 { get; }
        public virtual bool ActionL2 { get; }
        public virtual bool ActionR1 { get; }
        public virtual bool ActionR2 { get; }

        public virtual bool Char1 { get; }
        public virtual bool Char2 { get; }
        public virtual bool Char3 { get; }
        public virtual bool Char4 { get; }
    }
}