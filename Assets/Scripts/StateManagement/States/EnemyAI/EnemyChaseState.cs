using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemyChaseState : IState
    {
        EnemyAIController enemy;
        StateMachine esm;

        EnemyInputController input;
        PartyController party;

        public EnemyChaseState(EnemyAIController enemy)
        {
            this.enemy = enemy;
            esm = enemy.sm;

            party = enemy.Party;
            input = enemy.InputController;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {

        }

        public void ExecuteFrame()
        {
            enemy.AttackTimer.Tick();

            input.OnMove(enemy.DirToPlayer.normalized);

            if (!enemy.InChaseRange) esm.ChangeState(StateID.EnemyIdle);
            else if (enemy.InAttackRange)
            {
                if (enemy.AttackTimer.Full)
                    esm.ChangeState(StateID.EnemyAttack);
                else esm.ChangeState(StateID.EnemyStrafe);
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

        }
        #endregion
    }
}