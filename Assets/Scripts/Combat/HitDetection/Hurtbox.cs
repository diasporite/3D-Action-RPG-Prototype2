using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum HurtboxState
    {
        Idle = 0,
        Acting = 1,
        Guard = 2,
        Roll = 3,
    }

    public class Hurtbox : MonoBehaviour
    {
        [field: SerializeField] public HurtboxState HurtboxState { get; private set; }

        public IDamageable Damageable { get; private set; }

        private void Awake()
        {
            Damageable = GetComponentInParent<IDamageable>();
        }
    }
}