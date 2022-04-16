using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Combat/Character")]
    public class CharData : ScriptableObject
    {
        public string charName;

        public int baseVit = 50;
        public int baseEnd = 50;

        public int baseAtk = 50;
        public int baseDef = 50;

        public int weight = 128;

        public int baseHealthRegen = 4;
        public int baseStaminaRegen = 32;
    }
}