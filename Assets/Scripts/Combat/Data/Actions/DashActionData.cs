using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "New Dash", menuName = "Combat/Actions/Dash")]
    public class DashActionData : ActionData
    {
        [field: SerializeField] public float DashSpeed { get; private set; } = 5f;
        [field: SerializeField] public float DashLength { get; private set; } = 0.5f;
    }
}