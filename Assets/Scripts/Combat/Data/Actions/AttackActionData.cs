using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class DamageData
    {
        [field: Range(1, 5)]
        [field: SerializeField] public int NumOfHits { get; private set; } = 1;
        [field: SerializeField] public int BaseHealthDamage { get; private set; } = 40;
        [field: SerializeField] public int BaseStaminaDamage { get; private set; } = 10;

        public DamageInfo GetDamage(Combatant instigator) => 
            new DamageInfo(instigator, BaseHealthDamage, BaseStaminaDamage);
    }

    [System.Serializable]
    public class KnockbackData
    {
        [field: SerializeField] public float KnockbackForce { get; private set; } = 2f;
        [field: SerializeField] public float DragTime { get; private set; } = 0.2f;

        [field: Tooltip("From a 2D plane - x component is forward/backward component " +
            "(z axis in transform space), y component is up/down component")]
        [field: SerializeField] public Vector2 KnockbackDir { get; private set; }

        public Knockback Knockback(Vector3 forward) => new Knockback(this, forward);
    }

    [CreateAssetMenu(fileName = "New Attack", menuName = "Combat/Actions/Attack")]
    public class AttackActionData : ActionData
    {
        [field: SerializeField] public DamageData DamageData { get; private set; }
        [field: SerializeField] public KnockbackData KnockbackData { get; private set; }

        public override DamageInfo Info(Combatant instigator) => 
            DamageData.GetDamage(instigator);
        public override Knockback Knockback(Vector3 forward) => KnockbackData.Knockback(forward);
    }
}