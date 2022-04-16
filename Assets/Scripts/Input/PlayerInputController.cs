using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PlayerInputController : InputController
    {
        public readonly DirectionInput moveChar = new DirectionInput();
        public readonly DirectionInput moveCam = new DirectionInput();

        [Header("Face Buttons")]
        public string defendKey = "k";
        public string runKey = "l";
        public readonly PressInput defend = new PressInput("k", QueueAction.Defend);
        public readonly HoldInput run = new HoldInput("l");

        [Header("Lock On")]
        public string toggleLockKey = "right shift";
        public string selectNextKey = "right arrow";
        public string selectPreviousKey = "left arrow";
        public readonly PressInput toggleLock = new PressInput("right shift");
        public readonly PressInput selectNext = new PressInput("right arrow");
        public readonly PressInput selectPrevious = new PressInput("left arrow");

        [Header("Action Buttons (Triggers)")]
        public string actionL1Key = "u";
        public string actionL2Key = "y";
        public string actionR1Key = "o";
        public string actionR2Key = "p";
        public readonly PressInput actionL1 = new PressInput("u", QueueAction.ActionL1);
        public readonly PressInput actionL2 = new PressInput("y", QueueAction.ActionL2);
        public readonly PressInput actionR1 = new PressInput("o", QueueAction.ActionR1);
        public readonly PressInput actionR2 = new PressInput("p", QueueAction.ActionR2);

        [Header("Char Buttons (D-Pad)")]
        public string char1Key = "z";
        public string char2Key = "x";
        public string char3Key = "c";
        public string char4Key = "v";
        public readonly PressInput char1 = new PressInput("z", QueueAction.Char1);
        public readonly PressInput char2 = new PressInput("x", QueueAction.Char2);
        public readonly PressInput char3 = new PressInput("c", QueueAction.Char3);
        public readonly PressInput char4 = new PressInput("v", QueueAction.Char4);

        public override Vector2 InputMoveCharDir => moveChar.Dir;
        public override Vector3 MoveCharDir => moveChar.DirXz;

        public override Vector2 InputMoveCamDir => moveCam.Dir;
        public override Vector3 MoveCamDir => moveCam.DirXz;

        public override bool Defend() => defend.GetInput;
        public override bool Run() => run.GetInput;

        public override bool ToggleLock() => toggleLock.GetInput;
        public override bool SelectNext() => selectNext.GetInput;
        public override bool SelectPrevious() => selectPrevious.GetInput;

        public override bool ActionL1() => actionL1.GetInput;
        public override bool ActionL2() => actionL2.GetInput;
        public override bool ActionR1() => actionR1.GetInput;
        public override bool ActionR2() => actionR2.GetInput;

        public override bool Char1() => char1.GetInput;
        public override bool Char2() => char2.GetInput;
        public override bool Char3() => char3.GetInput;
        public override bool Char4() => char4.GetInput;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            defend.key = defendKey;
            run.key = runKey;

            toggleLock.key = toggleLockKey;
            selectNext.key = selectNextKey;
            selectPrevious.key = selectPreviousKey;

            actionL1.key = actionL1Key;
            actionL2.key = actionL2Key;
            actionR1.key = actionR1Key;
            actionR2.key = actionR2Key;

            char1.key = char1Key;
            char2.key = char2Key;
            char3.key = char3Key;
            char4.key = char4Key;
        }
    }
}