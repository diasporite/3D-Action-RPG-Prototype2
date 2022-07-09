using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "New Action", menuName = "Combat/Action")]
    public class ActionData : ScriptableObject
    {
        [field: Header("Animation Info")]
        [field: SerializeField] public AnimationData Animation { get; protected set; }

        [field: Header("Costs")]
        [field: SerializeField] public int SpCost { get; protected set; } = 20;
        [field: SerializeField] public int MpCost { get; protected set; } = 5;
    }
}