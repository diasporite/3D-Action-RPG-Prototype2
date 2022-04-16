using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Combatant : MonoBehaviour, IDamageable
    {
        [SerializeField] CharData data;

        [SerializeField] string charName;

        #region Stats
        [SerializeField] Stat vitality = new Stat(100, 255);
        [SerializeField] Stat endurance = new Stat(100, 255);

        [SerializeField] Stat attack = new Stat(100, 255);
        [SerializeField] Stat defence = new Stat(100, 255);

        [SerializeField] Stat weight = new Stat(128, 255);

        [SerializeField] Stat healthRegen = new Stat(4, 15);
        [SerializeField] Stat staminaRegen = new Stat(32, 63);
        #endregion

        #region StatAtLv
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
            
        }

        public void Init(CharData data)
        {
            charName = data.charName;

            vitality = new Stat(data.baseVit, 255);
            endurance = new Stat(data.baseEnd, 255);

            attack = new Stat(data.baseAtk, 255);
            defence = new Stat(data.baseDef, 255);

            weight = new Stat(data.weight, 255);

            healthRegen = new Stat(data.baseHealthRegen, 15);
            staminaRegen = new Stat(data.baseHealthRegen, 63);

            combat = GameManager.instance.combat;
        }
    }
}