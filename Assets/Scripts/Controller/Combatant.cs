using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Combatant : MonoBehaviour, IDamageable
    {
        [SerializeField] CharData data;

        [SerializeField] string charName;

        [Header("Progression")]
        [SerializeField] int level = 1;
        [SerializeField] int exp = 0;
        [SerializeField] int[] expAtLv;

        #region Stats
        [Header("Resource Stats")]
        [SerializeField] Stat vitality = new Stat(100, 255);
        [SerializeField] Stat endurance = new Stat(100, 255);

        [Header("Damage Stats")]
        [SerializeField] Stat attack = new Stat(100, 255);
        [SerializeField] Stat defence = new Stat(100, 255);

        [SerializeField] Stat weight = new Stat(128, 255);

        [Header("Regen Stats")]
        [SerializeField] Stat healthRegen = new Stat(4, 15);
        [SerializeField] Stat staminaRegen = new Stat(32, 63);
        #endregion

        #region StatAtLv
        [Header("Stats at Level")]
        [SerializeField] int[] vitAtLv;
        [SerializeField] int[] endAtLv;

        [SerializeField] int[] atkAtLv;
        [SerializeField] int[] defAtLv;

        [SerializeField] int[] hRegenAtLv;
        [SerializeField] int[] sRegenAtLv;
        #endregion

        PartyController party;
        Health health;
        Stamina stamina;

        CombatDatabase combat;

        public Stat Vitality => vitality;
        public Stat Endurance => endurance;

        public Stat Attack => attack;
        public Stat Defence => defence;

        public Stat Weight => weight;

        public void OnDamage(DamageInfo damage)
        {
            int hDamage = combat.HealthDamage(damage, this);
            int sDamage = combat.StaminaDamage(damage, this);
        }

        public void Init(int exp)
        {
            combat = GameManager.instance.combat;

            party = GetComponentInParent<PartyController>();
            health = party.Health;
            stamina = party.Stamina;

            charName = data.charName;

            expAtLv = combat.GetStatAtLv(StatID.ExpAtLv, data.baseExpAtLv);
            
            vitAtLv = combat.GetStatAtLv(StatID.Vitality, data.baseVit);
            endAtLv = combat.GetStatAtLv(StatID.Endurance, data.baseEnd);

            atkAtLv = combat.GetStatAtLv(StatID.Attack, data.baseAtk);
            defAtLv = combat.GetStatAtLv(StatID.Defence, data.baseDef);

            hRegenAtLv = combat.GetStatAtLv(StatID.HealthRegen, data.baseHealthRegen);
            sRegenAtLv = combat.GetStatAtLv(StatID.StaminaRegen, data.baseStaminaRegen);

            this.exp = exp;

            level = CalculateLv();

            vitality = new Stat(vitAtLv[level - 1], 255);
            endurance = new Stat(endAtLv[level - 1], 255);

            attack = new Stat(atkAtLv[level - 1], 255);
            defence = new Stat(defAtLv[level - 1], 255);

            weight = new Stat(data.weight, 255);

            healthRegen = new Stat(hRegenAtLv[level - 1], 15);
            staminaRegen = new Stat(sRegenAtLv[level - 1], 63);
        }

        int CalculateLv()
        {
            for (int i = 0; i < expAtLv.Length; i++)
                if (exp < expAtLv[i])
                    return i;

            return expAtLv.Length;
        }
    }
}