using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum StatID
    {
        None = 0,

        Health = 1,
        Stamina = 2,

        Level = 5,
        ExpToLv = 6,
        ExpReward = 7,

        Vitality = 10,
        Endurance = 11,

        Attack = 15,
        Defence = 16,

        Weight = 20,

        HealthRegen = 25,
        StaminaRegen = 26,
    }

    [CreateAssetMenu(fileName = "CombatDataase", menuName = "Database/Combat")]
    public class CombatDatabase : ScriptableObject
    {
        int levelCap = 10;

        [Header("Damage Multipliers")]
        public float critical = 1.4f;
        public float weakness = 1.25f;
        public float resistance = 0.8f;
        public float immunity = 0f;
        public float stab = 1.14f;

        [Header("Constants")]
        public float atkDefConst = 1f;
        public float dwtConst = 1f;

        public void InitDatabase()
        {

        }

        int WholeNumber(int value)
        {
            if (value > 0) return value;
            return 0;
        }

        int AtkDef(Combatant instigator, Combatant target)
        {
            return Mathf.RoundToInt(atkDefConst * (instigator.Attack.CurrentStatValue - 
                target.Defence.CurrentStatValue));
        }

        int WeightDiff(Combatant instigator, Combatant target)
        {
            return Mathf.RoundToInt(dwtConst * WholeNumber(instigator.Weight.CurrentStatValue - 
                target.Weight.CurrentStatValue));
        }

        public int HealthDamage(DamageInfo damage, Combatant target)
        {
            return WholeNumber(damage.HealthDamage + AtkDef(damage.Instigator, target) + 
                WeightDiff(damage.Instigator, target)) + 1;
        }

        public int StaminaDamage(DamageInfo damage, Combatant target)
        {
            return WholeNumber(damage.StaminaDamage + AtkDef(damage.Instigator, target) +
                WeightDiff(damage.Instigator, target)) + 1;
        }

        public int[] GetStatAtLv(StatID stat, int baseValue)
        {
            int[] statAtLv = new int[levelCap];

            for(int i = 0; i < statAtLv.Length; i++)
            {
                switch (stat)
                {
                    case StatID.Vitality:
                        break;
                    case StatID.Endurance:
                        break;
                    case StatID.Attack:
                        break;
                    case StatID.Defence:
                        break;
                    default:
                        break;
                }
            }

            return statAtLv;
        }
    }
}