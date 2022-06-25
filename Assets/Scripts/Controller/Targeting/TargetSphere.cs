using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class TargetSphere : MonoBehaviour
    {
        [field: SerializeField] public bool Active { get; set; } = false;

        PartyController party;

        [Range(0f, 1f)]
        [SerializeField] float focalPoint = 0.5f;
        [SerializeField] Transform targetFocus;

        [field: SerializeField] public List<Target> Targets { get; private set; } = 
            new List<Target>();

        Camera mainCam;

        [field: SerializeField] public Target CurrentTarget { get; private set; }

        public Transform TargetFocus => targetFocus;

        public Transform CurrentTargetTransform => CurrentTarget?.transform;

        public bool NoTargets => Targets.Count <= 0;

        private void Awake()
        {
            party = GetComponentInParent<PartyController>();

            mainCam = Camera.main;
        }

        private void Update()
        {
            if (CurrentTarget == null) Active = false;
            else
            {
                if (party.CurrentControllerTransform != null)
                {
                    transform.position = party.CurrentControllerTransform.position;
                    TargetFocus.transform.position =
                        Vector3.Lerp(party.CurrentControllerTransform.position,
                        CurrentTargetTransform.position, focalPoint);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponentInChildren<Target>();

            if (target != null)
                if (target.transform.root != transform.root && !Targets.Contains(target))
                    Targets.Add(target);
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.GetComponentInChildren<Target>();

            if (target != null && Targets.Contains(target)) Targets.Remove(target);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.5f * transform.lossyScale.x);
        }

        public bool FindTarget()
        {
            Target closestTarget = null;
            float closestSqrDist = Mathf.Infinity;

            foreach (var target in Targets)
            {
                Vector2 screenPos = mainCam.WorldToViewportPoint(target.transform.position);

                if (OutsideScreen(screenPos)) continue;

                var dirToCentre = 0.5f * Vector2.one - screenPos;

                if (dirToCentre.sqrMagnitude < closestSqrDist)
                {
                    closestTarget = target;
                    closestSqrDist = dirToCentre.sqrMagnitude;
                }
            }

            if (closestTarget == null) return false;

            CurrentTarget = closestTarget;

            return true;
        }

        public void SelectTarget(int index)
        {

        }

        bool OutsideScreen(Vector2 screenPos)
        {
            return screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1;
        }
    }
}