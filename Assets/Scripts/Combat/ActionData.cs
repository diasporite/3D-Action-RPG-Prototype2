using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    //[CreateAssetMenu(fileName = "New Action", menuName = "Combat/Action")]
    public class ActionData : ScriptableObject
    {
        [Header("Animation Info")]
        public AnimationClip clip;
        [Tooltip("The speed at which the controllers moves (in the direction " +
            "of motion) if movement is allowed during the action. For some actions," +
            " speed is the inverse of a specific time period")]
        public float speed = 2f;

        [Header("Stamina")]
        public float spCost = 20;

        [Header("Usage")]
        public bool infiniteUse = false;
        [Range(1, 40)]
        public int uses = 20;
    }
}