using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    //[CreateAssetMenu(fileName = "New Action", menuName = "Combat/Action")]
    public class ActionData : ScriptableObject
    {
        [Header("Animation Info")]
        public AnimationData animation;

        [Header("Stamina")]
        public float spCost = 20;

        [Header("Usage")]
        public bool infiniteUse = false;
        [Range(1, 40)]
        public int uses = 20;
    }
}