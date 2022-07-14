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

            enemy.Agent.updatePosition = false;
            enemy.Agent.updateRotation = false;
        }

        #region InterfaceMethods
        public void Enter(params object[] args)
        {

        }

        public void ExecuteFrame()
        {
            enemy.AttackTimer.Tick();

            MoveToPlayer();

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
            enemy.Agent.ResetPath();
            enemy.Agent.velocity = Vector3.zero;
        }
        #endregion

        void MoveToPlayer()
        {
            enemy.Agent.destination = enemy.PlayerTransform.position;

            var dirToPlayer = enemy.Agent.desiredVelocity.normalized;
            input.OnMove(dirToPlayer);

            //enemy.Nma.velocity = enemy.Party.GetComponent<CharacterController>().velocity;
            enemy.Agent.velocity = enemy.Party.CurrentController.GetComponent<Movement>().MoveVelocity;
        }
    }
}