using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] protected int statValue;
        [SerializeField] protected int currentStatValue;

        protected int statCap = 255;

        List<StatModifier> modifiers = new List<StatModifier>();

        public int StatValue => statValue;
        public int CurrentStatValue => currentStatValue;

        public Stat(int statValue)
        {
            this.statValue = statValue;
            statCap = statValue;
            currentStatValue = statValue;
        }

        public Stat(int statValue, int statCap)
        {
            this.statCap = statCap;
            this.statValue = Mathf.Clamp(statValue, 1, statCap);
            currentStatValue = statValue;
        }

        public void ApplyStatModifier(StatModifier mod)
        {
            modifiers.Add(mod);

            foreach(var m in modifiers)
            {
                switch (m.Type)
                {
                    case StatModifierType.Additive:
                        currentStatValue = Mathf.Clamp(statValue + m.Amount, 1, statCap);
                        break;
                    case StatModifierType.PercentSimple:
                        currentStatValue = 
                            Mathf.Clamp(Mathf.RoundToInt(statValue + 0.01f * m.Amount), 
                            1, statCap);
                        break;
                    case StatModifierType.PercentCompound:
                        currentStatValue = 
                            Mathf.Clamp(Mathf.RoundToInt((1 + 0.01f * m.Amount) * statValue), 
                            1, statCap);
                        break;
                }
            }
        }
    }
}