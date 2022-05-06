using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class TargetSphere : MonoBehaviour
    {
        [SerializeField] List<Target> targets = new List<Target>();

        [SerializeField] Target currentTarget;
        public Target CurrentTarget { get; private set; }
        //public Transform CurrentTargetTransform => CurrentTarget?.transform;
        public Transform CurrentTargetTransform
        {
            get
            {
                if (CurrentTarget != null) return CurrentTarget.transform;
                return null;
            }
        }

        public bool NoTargets => targets.Count <= 0;

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
            Gizmos.DrawWireSphere(transform.position, GetComponent<SphereCollider>().radius);
        }

        public void SelectTarget()
        {
            if (NoTargets) return;

            CurrentTarget = targets[0];
            currentTarget = CurrentTarget;
        }
    }
}