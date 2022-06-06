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

        Camera mainCam;

        [field: SerializeField] public Target CurrentTarget { get; private set; }

        public Transform CurrentTargetTransform => CurrentTarget?.transform;

        public bool NoTargets => targets.Count <= 0;

        private void Awake()
        {
            party = GetComponentInParent<PartyController>();

            mainCam = Camera.main;
        }

        private void Update()
        {
            if (CurrentTarget != null && party.CurrentControllerTransform != null)
            {
                transform.position = party.CurrentControllerTransform.position;
                targetFocus.transform.position = 0.5f * (party.CurrentControllerTransform.position +
                    CurrentTargetTransform.position);
            }
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

            //float closestSqrDist = Mathf.Infinity;

            //CurrentTarget = null;

            //foreach(var target in targets)
            //{
            //    Vector2 screenPos = mainCam.WorldToViewportPoint(target.transform.position);

            //    if (OutsideScreen(screenPos)) continue;

            //    var dirToCentre = 0.5f * Vector2.one - screenPos;

            //    if (dirToCentre.sqrMagnitude < closestSqrDist)
            //    {
            //        CurrentTarget = target;
            //        closestSqrDist = dirToCentre.sqrMagnitude;
            //    }
            //}

            CurrentTarget = targets[0];
        }

        bool OutsideScreen(Vector2 screenPos)
        {
            return screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1;
        }
    }
}