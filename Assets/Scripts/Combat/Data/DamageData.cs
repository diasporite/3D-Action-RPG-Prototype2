using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class DamageData
    {
        [Range(1, 5)]
        [SerializeField] int numOfHits = 1;

        public DamageInfo GetDamage(Combatant instigator) => null;
    }
}