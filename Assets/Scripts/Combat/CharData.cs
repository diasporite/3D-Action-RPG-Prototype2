using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Combat/Character")]
    public class CharData : ScriptableObject
    {
        [Header("Info")]
        public string charName;
        public Sprite portrait;

        [Header("Progression")]
        public int baseExpAtLv = 50;

        [Header("Resource Stats")]
        public int baseVit = 50;
        public int baseEnd = 50;

        [Header("Combat Stats")]
        public int baseAtk = 50;
        public int baseDef = 50;
        [Range(1, 255)]
        public int weight = 128;

        [Header("Regen Stats")]
        public int baseHealthRegen = 4;
        public int baseStaminaRegen = 32;

        [Header("Actions")]
        public ActionData[] actions;
        public ActionData defendAction;
    }
}