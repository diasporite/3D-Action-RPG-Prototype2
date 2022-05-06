using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class TargetSphere : MonoBehaviour
    {
        [SerializeField] List<Target> targets = new List<Target>();

        public Target CurrentTarget { get; private set; }
        public Transform CurrentTargetTransform => CurrentTarget?.transform;

        private void Update()
        {
            SelectTarget();
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<Target>();

            if (target != null) targets.Add(target);
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.GetComponent<Target>();

            if (target != null && targets.Contains(target)) targets.Remove(target);
        }

        public void SelectTarget()
        {
            if (targets.Count <= 0) return;

            CurrentTarget = targets[0];
        }
    }
}