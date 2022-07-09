using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemyStrafeState : IState
    {
        EnemyAIController enemy;
        StateMachine esm;

        EnemyInputController input;
        PartyController party;

        public EnemyStrafeState(EnemyAIController enemy)
        {
            this.enemy = enemy;
            esm = enemy.sm;

            party = enemy.Party;
            input = enemy.InputController;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            input.OnToggleLock();
        }

        public void ExecuteFrame()
        {
            enemy.AttackTimer.Tick();

            input.OnMove(Vector3.right);

            if (enemy.AttackTimer.Full) esm.ChangeState(StateID.EnemyAttack);
            else
            {
                if (!enemy.InAttackRange && enemy.InChaseRange)
                    esm.ChangeState(StateID.EnemyChase);
                else if (!enemy.InChaseRange) esm.ChangeState(StateID.EnemyIdle);
            }
        }

        public void ExecuteFrameFixed()
        {

        }

        public void ExecuteFrameLate()
        {

        }

        public void Exit()
        {
            input.OnToggleLock();
        }
        #endregion
    }
}