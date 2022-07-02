using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Target : MonoBehaviour
    {
        PartyController party;

        public readonly List<TargetSphere> targetSpheres = new List<TargetSphere>();

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

        public void AddTargetSphere(TargetSphere ts)
        {
            targetSpheres.Add(ts);
        }

        public void RemoveTargetSphere(TargetSphere ts)
        {
            if (targetSpheres.Contains(ts))
                targetSpheres.Remove(ts);
        }

        void RemoveTarget()
        {
            print(9);
            foreach (var ts in targetSpheres)
                ts.RemoveTarget(this);
        }
    }
}