using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class AnimationMoveData
    {
        [field: Range(0f, 1f)]
        [field: SerializeField] public float StartInNormTime { get; private set; } = 0f;
        [field: SerializeField] public float ForwardSpeed { get; private set; } = 5f;
        [field: SerializeField] public float DragTime { get; private set; } = 0f;

        public AnimationMoveData()
        {
            StartInNormTime = 0f;
            ForwardSpeed = 0f;
            DragTime = 0f;
        }
    }

    [System.Serializable]
    public class AnimationData
    {
        [field: SerializeField] public AnimationClip animation { get; private set; }
        [field: SerializeField] public AnimationMoveData[] motion { get; private set; }

        public AnimationMoveData CurrentMoveData(float t)
        {
            if (motion.Length > 0)
            {
                for (int i = 0; i < motion.Length; i++)
                    if (motion[i].StartInNormTime > t)
                        if (i != 0)
                            return motion[i - 1];
            }

            return new AnimationMoveData();
        }
    }
}