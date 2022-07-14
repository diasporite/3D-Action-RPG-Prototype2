using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemyAttackState : IState
    {
        EnemyAIController enemy;
        StateMachine esm;

        EnemyInputController input;
        PartyController party;

        public EnemyAttackState(EnemyAIController enemy)
        {
            this.enemy = enemy;
            esm = enemy.sm;

            party = enemy.Party;
            input = enemy.InputController;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            input.OnLockOn();

            input.OnAttack(Random.Range(0, enemy.Party.CurrentCombatant.Skillset.Count - 1));
            enemy.AttackTimer.Reset();
        }

        public void ExecuteFrame()
        {
            MoveAgentToPlayer();

            if (!enemy.ActionQueue.Executing)
            {
                if (enemy.InAttackRange) esm.ChangeState(StateID.EnemyStrafe);
                else if (!enemy.InAttackRange && enemy.InChaseRange) esm.ChangeState(StateID.EnemyChase);
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
            input.OnLockOff();
        }
        #endregion

        void MoveAgentToPlayer()
        {
            enemy.Agent.velocity = enemy.Party.CurrentController.GetComponent<Movement>().MoveVelocity;
        }
    }
}