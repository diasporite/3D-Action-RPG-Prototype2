using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField] float timerLength = 3f;

        [SerializeField] float chaseRange = 6f;
        [SerializeField] float attackRange = 1f;

        [field: SerializeField] public Cooldown Timer { get; private set; }

        [field: SerializeField] public Transform playerTransform { get; private set; }

        public float SqrChaseRange { get; private set; }
        public float SqrAttackRange { get; private set; }

        public PartyController Party { get; private set; }
        public ActionQueue ActionQueue { get; private set; }
        public EnemyInputController InputController { get; private set; }

        public readonly StateMachine sm = new StateMachine();

        public Vector3 DirToPlayer
        {
            get
            {
                var ds = playerTransform.position - transform.position;
                ds.y = 0;
                return ds;
            }
        }

        public float SqrDistToPlayer => DirToPlayer.sqrMagnitude;

        public bool InChaseRange => SqrDistToPlayer <= SqrChaseRange;
        public bool InAttackRange => SqrDistToPlayer <= SqrAttackRange;

        private void Awake()
        {
            Timer = new Cooldown(timerLength);

            Party = GetComponent<PartyController>();
            ActionQueue = GetComponent<ActionQueue>();
            InputController = GetComponent<EnemyInputController>();

            SqrChaseRange = chaseRange * chaseRange;
            SqrAttackRange = attackRange * attackRange;
        }

        private void Start()
        {
            sm.AddState(StateID.EnemyIdle, new EnemyIdleState(this));
            sm.AddState(StateID.EnemyChase, new EnemyChaseState(this));
            sm.AddState(StateID.EnemyAttack, new EnemyAttackState(this));

            sm.ChangeState(StateID.EnemyIdle);
        }

        private void Update()
        {
            sm.Update();
        }

        private void OnEnable()
        {
            playerTransform = FindObjectOfType<PlayerSpawner>().transform;
        }

        private void OnDisable()
        {
            
        }
    }
}