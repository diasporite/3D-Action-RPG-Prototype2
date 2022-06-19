using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class AnimationMoveData
    {
        [Range(0f, 1f)]
        public float startInNormTime = 0f;
        public float forwardSpeed = 5f;
    }

    [System.Serializable]
    public class AnimationData
    {
        [field: SerializeField] public AnimationClip animation { get; private set; }
        [field: SerializeField] public AnimationMoveData[] motion { get; private set; }
    }
}