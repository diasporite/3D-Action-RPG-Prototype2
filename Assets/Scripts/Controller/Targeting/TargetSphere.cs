using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class TargetSphere : MonoBehaviour
    {
        PartyController party;

        [SerializeField] Transform targetFocus;

        [SerializeField] List<Target> targets = new List<Target>();

        [SerializeField] Target currentTarget;

        public Target CurrentTarget { get; private set; }

        public Transform CurrentTargetTransform => CurrentTarget?.transform;

        public bool NoTargets => targets.Count <= 0;

        private void Awake()
        {
            party = GetComponentInParent<PartyController>();
        }

        private void Update()
        {
            if (CurrentTarget != null)
                targetFocus.transform.position = 0.5f * (party.CurrentControllerTransform.position + 
                    CurrentTargetTransform.position);
        }

        private void OnEnable()
        {
            SelectTarget();
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponentInChildren<Target>();

            if (target != null)
                if (target.transform.root != transform.root && !targets.Contains(target))
                    targets.Add(target);
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.GetComponentInChildren<Target>();

            if (target != null && targets.Contains(target)) targets.Remove(target);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.5f * transform.lossyScale.x);
        }

        public void SelectTarget()
        {
            if (NoTargets) return;

            CurrentTarget = targets[0];
            currentTarget = CurrentTarget;
        }
    }
}