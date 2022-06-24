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

        }

        public void ExecuteFrame()
        {
            if (!enemy.InAttackRange && enemy.InChaseRange) esm.ChangeState(StateID.EnemyChase);
            else if (!enemy.InChaseRange) esm.ChangeState(StateID.EnemyIdle);

            if (enemy.Timer.Full)
            {
                input.OnAttack(1);
                enemy.Timer.Reset();
            }

            if (!enemy.ActionQueue.Executing) enemy.Timer.Tick();

            input.OnMove(Vector3.zero);
            party.CurrentController.Move(false);
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