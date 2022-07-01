using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "New Action", menuName = "Combat/Action")]
    public class ActionData : ScriptableObject
    {
        [field: Header("Animation Info")]
        [field: SerializeField] public AnimationData Animation { get; private set; }

        [field: Header("Costs")]
        [field: SerializeField] public int SpCost { get; private set; } = 20;
        [field: SerializeField] public int MpCost { get; private set; } = 5;
    }
}