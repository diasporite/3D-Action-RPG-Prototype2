using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class Statf
    {
        [field: SerializeField] public float StatValue { get; protected set; }
        [field: SerializeField] public float CurrentStatValue { get; protected set; }

        protected float statCap = 255;

        List<StatModifier> modifiers = new List<StatModifier>();

        public Statf(int statValue)
        {
            StatValue = statValue;
            statCap = statValue;
            CurrentStatValue = statValue;
        }

        public Statf(int statValue, int statCap)
        {
            this.statCap = statCap;
            StatValue = Mathf.Clamp(statValue, 1, statCap);
            CurrentStatValue = statValue;
        }

        public void ApplyStatModifier(StatModifier mod)
        {
            modifiers.Add(mod);

            foreach(var m in modifiers)
            {
                switch (m.Type)
                {
                    case StatModifierType.Additive:
                        CurrentStatValue = Mathf.Clamp(StatValue + m.Amount, 1, statCap);
                        break;
                    case StatModifierType.PercentSimple:
                        CurrentStatValue = 
                            Mathf.Clamp(Mathf.RoundToInt(StatValue + 0.01f * m.Amount), 
                            1, statCap);
                        break;
                    case StatModifierType.PercentCompound:
                        CurrentStatValue = 
                            Mathf.Clamp(Mathf.RoundToInt((1 + 0.01f * m.Amount) * StatValue), 
                            1, statCap);
                        break;
                }
            }
        }
    }
}