using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Target : MonoBehaviour
    {
        PartyController party;

        public TargetSphere TargetSphere { get; set; }

        private void Awake()
        {
            party = GetComponentInParent<PartyController>();
        }

        private void OnEnable()
        {
            party.OnDeath += RemoveTarget;
        }

        private void OnDisable()
        {
            party.OnDeath -= RemoveTarget;
        }

        void RemoveTarget()
        {
            TargetSphere?.RemoveTarget(this);
        }
    }
}