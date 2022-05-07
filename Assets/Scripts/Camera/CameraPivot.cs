using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CameraPivot : MonoBehaviour
    {
        public TargetSphere targetSphere;

        public LayerMask targetMask;

        [SerializeField] float lockOnRange = 12f;
        float sqrLockOnRange;

        public float offsetTheta = 20f;

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
        InputController inputController;

        public float Theta => theta;

        public bool InRange
        {
            get
            {
                if (currentTarget != null)
                    return (currentTarget.position - follow.position).sqrMagnitude < 
                        sqrLockOnRange;

                return false;
            }
        }

        public Controller CurrentController => party.CurrentController;

        private void Awake()
        {
            party = GetComponentInParent<PartyController>();
            inputController = GetComponentInParent<InputController>();

            sqrLockOnRange = lockOnRange * lockOnRange;
        }

        private void Update()
        {
            if (!targetSphere.enabled)
            {
                GetInput();
                MovePivot();
            }
            else
            {
                currentTarget = targetSphere.CurrentTargetTransform;
                FollowTarget();
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

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(follow.transform.position, camDist);
        }

        public void Init()
        {
            follow = CurrentController.transform;
            transform.position = follow.position - camDist * follow.forward + heightDist * follow.up;
        }

        void GetInput()
        {
            dx = inputController.MoveCam.x;
            dy = inputController.MoveCam.y;
        }

        void MovePivot()
        {
            theta += rotateSpeed * dx * Time.deltaTime;
            theta = theta % 360;

            height += heightSpeed * dy * Time.deltaTime;
            height = Mathf.Clamp(height, -maxHeight, maxHeight);

            follow = CurrentController.transform;

            newPos = ThetaToPosition(theta, heightDist + height, follow);

            transform.position = Vector3.MoveTowards(transform.position, newPos,
                updateSpeed * Time.deltaTime);

            targetPos = follow.position;
        }

        void FollowTarget()
        {
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

            newPos = ThetaToPosition(theta + offsetTheta, heightDist + height, follow);

            transform.position = Vector3.MoveTowards(transform.position, newPos,
                updateSpeed * Time.deltaTime);
        }

        public void ToggleLock()
        {
            if (targetSphere.NoTargets) return;

            targetSphere.enabled = !targetSphere.enabled;

            //CurrentController.Model.SetAnimLocked(targetSphere.enabled);
        }

        public void ToggleLock(bool value)
        {
            if (targetSphere.NoTargets) return;

            targetSphere.enabled = value;
        }

        public Vector3 ThetaToPosition(float theta, float height, Transform follow)
        {
            return newPos = follow.position +
                camDist * (-Mathf.Cos(theta * Mathf.Deg2Rad) * Vector3.forward +
                Mathf.Sin(theta * Mathf.Deg2Rad) * Vector3.right) +
                height * follow.transform.up;
        }
    }
}