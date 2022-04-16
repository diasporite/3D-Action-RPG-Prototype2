using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class DamageInfo
    {
        [SerializeField] Combatant instigator;

        [SerializeField] int healthDamage;
        [SerializeField] int staminaDamage;

        public Combatant Instigator => instigator;

        public int HealthDamage => healthDamage;
        public int StaminaDamage => staminaDamage;

        public DamageInfo(Combatant instigator, int healthDamage, int staminaDamage)
        {
            this.instigator = instigator;

            this.healthDamage = healthDamage;
            this.staminaDamage = staminaDamage;
        }
    }
}