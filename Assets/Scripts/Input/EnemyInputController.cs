using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemyInputController : InputController
    {
        public void OnMove(Vector3 dir)
        {
            MoveChar = new Vector2(dir.x, dir.z);
        }

        public void OnRotate(Vector3 dir)
        {

        }

        public void OnDpad(Vector2 dir)
        {
            Dpad = dir;

            if (Dpad != Vector2.zero) InvokeDpad(Dpad);
        }

        public void OnAttack(int index)
        {
            InvokeAction(index);
        }

        public void OnGuard()
        {
            //if (context.canceled) InvokeRoll();
            //else if (context.performed) InvokeGuard();
        }

        public void OnRun()
        {
            //if (context.canceled) InvokeWalk(MoveChar);
            //else if (context.performed) InvokeRun(MoveChar);
        }

        public void OnToggleLock()
        {
            //if (context.performed) InvokeLock();
        }
    }
}