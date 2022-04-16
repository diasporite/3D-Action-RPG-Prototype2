using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CameraPivot : MonoBehaviour
    {
        public bool locked = false;

        public LayerMask targetMask;

        [SerializeField] float lockOnRange = 12f;
        float sqrLockOnRange;

        public Vector3 offset = new Vector3(2, 0, -2);

        [Header("Speeds")]
        public float rotateSpeed = 120f;
        public float heightSpeed = 3f;
        public float updateSpeed = 20f;

        [Header("Distances")]
        public float camDist = 3f;
        public float heightDist = 2f;
        public float maxHeight = 1f;

        [Header("Transforms")]
        public Transform follow;
        public Transform currentTarget;
        public Transform[] targets;
        public float[] angles;
        public Vector3 targetPos;

        [Header("Variables")]
        [SerializeField] float theta = 0;
        [SerializeField] float height = 0;

        float dx = 0;
        float dy = 0;

        Vector3 newPos;
        Vector3 dirToFollow;
        Vector3 dirToTarget;

        PartyController party;

        public float Theta => theta;

        private void Awake()
        {
            party = GetComponentInParent<PartyController>();

            sqrLockOnRange = lockOnRange * lockOnRange;
        }

        private void Update()
        {
            LockOn();

            if (!locked)
            {
                GetInput();
                MovePivot();
            }
            else
            {
                var targetsFound = FindTargets();
                //print(targetsFound);
                if (targetsFound) FollowTarget();
                else ToggleLock(false);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(newPos, 0.5f);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(targetPos, 0.5f);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, camDist * dirToFollow);
            Gizmos.DrawRay(transform.position, camDist * dirToTarget);
            Gizmos.DrawRay(follow.transform.position, -camDist * follow.transform.forward);
        }

        public void Init()
        {
            follow = party.CurrentController.transform;
            transform.position = follow.position - camDist * follow.forward + heightDist * follow.up;
        }

        void GetInput()
        {
            dx = Input.GetAxis("Camera Horizontal");
            dy = Input.GetAxis("Camera Vertical");
        }

        void MovePivot()
        {
            theta += rotateSpeed * dx * Time.deltaTime;
            theta = theta % 360;

            height += heightSpeed * dy * Time.deltaTime;
            height = Mathf.Clamp(height, -maxHeight, maxHeight);

            follow = party.CurrentController.transform;

            newPos = follow.position -
                camDist * Mathf.Cos(theta * Mathf.Deg2Rad) * follow.forward +
                camDist * Mathf.Sin(theta * Mathf.Deg2Rad) * follow.right + 
                (heightDist + height) * transform.up;

            transform.position = Vector3.MoveTowards(transform.position, newPos,
                updateSpeed * Time.deltaTime);

            targetPos = follow.position;
        }

        void FollowTarget()
        {
            if (currentTarget == null)
            {
                locked = false;
                return;
            }

            follow = party.CurrentController.transform;

            targetPos = 0.5f * (follow.position + currentTarget.position);

            dirToFollow = follow.position - transform.position;
            dirToTarget = targetPos - follow.position;

            dirToFollow.y = 0;
            dirToTarget.y = 0;

            dirToFollow.Normalize();
            dirToTarget.Normalize();

            theta = Mathf.Atan2(-dirToTarget.x, dirToTarget.z) * Mathf.Rad2Deg;
            height = maxHeight;

            newPos = follow.position - camDist * dirToTarget + 
                heightDist * follow.transform.up + (follow.transform.rotation * offset);

            transform.position = Vector3.MoveTowards(transform.position, newPos,
                updateSpeed * Time.deltaTime);
        }

        public void LockOn()
        {
            if (currentTarget != null)
            {
                if ((currentTarget.position - follow.position).sqrMagnitude > sqrLockOnRange)
                    ToggleLock(false);
            }

            if (Input.GetKeyDown("right shift")) ToggleLock();
        }

        bool FindTargets()
        {
            var hits = Physics.OverlapSphere(follow.transform.position, 
                lockOnRange, targetMask);

            if (hits != null)
            {
                if (hits.Length > 0)
                {
                    targets = new Transform[hits.Length];
                    angles = new float[hits.Length];
                    var forward = follow.transform.forward;

                    for (int i = 0; i < targets.Length; i++)
                    {
                        targets[i] = hits[i].transform;
                        angles[i] = Vector3.SignedAngle(forward, 
                            targets[i].position, follow.transform.up);
                    }

                    currentTarget = targets[0];

                    return true;
                }
            }

            return false;
        }

        void ToggleLock()
        {
            locked = !locked;

            party.CurrentController.Model.SetAnimLocked(locked);
        }

        void ToggleLock(bool value)
        {
            locked = value;

            party.CurrentController.Model.SetAnimLocked(locked);
        }
    }
}