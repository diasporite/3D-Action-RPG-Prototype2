using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class TargetSphere : MonoBehaviour
    {
        public event Action OnLockOn;
        public event Action OnLockOff;

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
            if (NoTargets) Active = false;
            else
            {
                if (CurrentTarget != null && party.CurrentControllerTransform != null)
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
            {
                if (target.transform.root != transform.root && !Targets.Contains(target))
                {
                    Targets.Add(target);
                    target.TargetSphere = this;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.GetComponentInChildren<Target>();

            if (target != null && Targets.Contains(target))
            {
                target.TargetSphere = null;
                Targets.Remove(target);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.5f * transform.lossyScale.x);
        }

        public void InvokeLockOn()
        {
            OnLockOn?.Invoke();
        }

        public void InvokeLockOff()
        {
            OnLockOff?.Invoke();
        }

        public bool SelectTargets()
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

        bool OutsideScreen(Vector2 screenPos)
        {
            return screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1;
        }

        public void RemoveTarget(Target target)
        {
            if (Targets.Contains(target))
            {
                Targets.Remove(target);

                SelectTargets();

                if (Active)
                {
                    if (NoTargets) SetInactive();
                }
            }
        }

        void SetInactive()
        {
            Active = false;

            InvokeLockOff();
        }
    }
}