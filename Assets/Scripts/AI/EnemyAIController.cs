using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG_Project
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField] StateID currentState;

        [SerializeField] float attackDelay = 3f;
        [SerializeField] float spawnDelay = 20f;

        [SerializeField] float chaseRange = 6f;
        [SerializeField] float attackRange = 1f;

        [field: SerializeField] public Cooldown AttackTimer { get; private set; }
        [field: SerializeField] public Cooldown SpawnTimer { get; private set; }

        [field: SerializeField] public Transform PlayerTransform { get; private set; }

        public float SqrChaseRange { get; private set; }
        public float SqrAttackRange { get; private set; }

        public PartyController Party { get; private set; }
        public ActionQueue ActionQueue { get; private set; }
        public EnemyInputController InputController { get; private set; }
        public NavMeshAgent Agent { get; private set; }

        public readonly StateMachine sm = new StateMachine();

        public Vector3 DirToPlayer
        {
            get
            {
                var ds = PlayerTransform.position - transform.position;
                ds.y = 0;
                return ds;
            }
        }

        public float SqrDistToPlayer => DirToPlayer.sqrMagnitude;

        public bool InChaseRange => SqrDistToPlayer <= SqrChaseRange;
        public bool InAttackRange => SqrDistToPlayer <= SqrAttackRange;

        private void Awake()
        {
            AttackTimer = new Cooldown(attackDelay);
            SpawnTimer = new Cooldown(spawnDelay);

            Party = GetComponent<PartyController>();
            ActionQueue = GetComponent<ActionQueue>();
            InputController = GetComponent<EnemyInputController>();
            Agent = GetComponent<NavMeshAgent>();

            SqrChaseRange = chaseRange * chaseRange;
            SqrAttackRange = attackRange * attackRange;
        }

        private void Start()
        {
            sm.AddState(StateID.EnemyIdle, new EnemyIdleState(this));
            sm.AddState(StateID.EnemyChase, new EnemyChaseState(this));
            sm.AddState(StateID.EnemyAttack, new EnemyAttackState(this));
            sm.AddState(StateID.EnemyStandby, new EnemyStandbyState(this));
            sm.AddState(StateID.EnemyStrafe, new EnemyStrafeState(this));

            sm.ChangeState(StateID.EnemyIdle);
        }

        private void Update()
        {
            currentState = sm.CurrentStateKey;

            sm.Update();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, 3f * DirToPlayer.normalized);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, 3f * transform.forward);

            Gizmos.color = Color.magenta;
            if (Agent != null)
                Gizmos.DrawRay(transform.position, 3f * Agent.desiredVelocity);
        }

        private void OnEnable()
        {
            PlayerTransform = FindObjectOfType<PlayerSpawner>().transform;

            Party.OnDeath += SwitchToStandby;
        }

        private void OnDisable()
        {
            Party.OnDeath -= SwitchToStandby;
        }

        void SwitchToStandby()
        {
            sm.ChangeState(StateID.EnemyStandby);
        }
    }
}