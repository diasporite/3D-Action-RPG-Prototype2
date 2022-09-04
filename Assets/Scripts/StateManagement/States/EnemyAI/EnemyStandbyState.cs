using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemyStandbyState : IState
    {
        EnemyAIController enemy;
        StateMachine esm;

        EnemyInputController input;
        PartyController party;

        public EnemyStandbyState(EnemyAIController enemy)
        {
            this.enemy = enemy;
            esm = enemy.sm;

            party = enemy.Party;
            input = enemy.InputController;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {
            enemy.Agent.updatePosition = false;

            enemy.SpawnTimer.Reset();
        }

        public void ExecuteFrame()
        {
            enemy.SpawnTimer.Tick();

            if (enemy.SpawnTimer.Full)
            {
                Debug.Log("Enemy spawned");
                //esm.ChangeState(StateID.EnemyIdle);
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