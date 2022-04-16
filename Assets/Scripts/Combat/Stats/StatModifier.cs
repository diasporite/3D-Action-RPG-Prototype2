using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum StatModifierType
    {
        Additive = 0,
        PercentSimple = 1,
        PercentCompound = 2,
    }

    [System.Serializable]
    public class StatModifier
    {
        [SerializeField] StatModifierType type;
        [SerializeField] int amount;

        public StatModifierType Type => type;
        public int Amount => amount;

        public StatModifier(StatModifierType type, int amount)
        {
            this.type = type;
            this.amount = amount;
        }
    }
}