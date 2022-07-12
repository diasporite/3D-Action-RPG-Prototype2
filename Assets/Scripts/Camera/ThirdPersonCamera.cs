using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class CameraOrbit
    {
        [field: SerializeField] public float Height { get; private set; } = 1f;
        [field: SerializeField] public float Radius { get; private set; } = 3f;
    }

    public class ThirdPersonCamera : MonoBehaviour, ICamera
    {
        [SerializeField] public bool Locked { get; private set; }

        [SerializeField] float pitchSpeed = 1f;
        [SerializeField] float yawSpeed = 90f;

        [SerializeField] float height = 0f;  
        [SerializeField] float radius = 0f;
        [SerializeField] float theta = 0f;

        [field: SerializeField] public CameraOrbit TopOrbit { get; private set; }
        [field: SerializeField] public CameraOrbit MiddleOrbit { get; private set; }
        [field: SerializeField] public CameraOrbit BottomOrbit { get; private set; }

        [SerializeField] Transform freeTarget;
        [SerializeField] Transform lockTarget;

        InputController input;

        public Transform CurrentTarget
        {
            get
            {
                if (Locked) return lockTarget;

                return freeTarget;
            }
        }

        private void Awake()
        {
            input = GetComponentInParent<InputController>();

            height = MiddleOrbit.Height;
            radius = MiddleOrbit.Radius;
            theta = 0;
        }

        private void Update()
        {
            if (!Locked) Move(input.MoveCam);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            //DrawRadii(TopOrbit);
            //DrawRadii(MiddleOrbit);
            //DrawRadii(BottomOrbit);
        }

        void DrawRadii(CameraOrbit orbit)
        {
            Gizmos.DrawLine(transform.position + orbit.Height * transform.up - 
                orbit.Radius * transform.forward, transform.position + orbit.Height * transform.up +
                orbit.Radius * transform.forward);
            Gizmos.DrawLine(transform.position + orbit.Height * transform.up - 
                orbit.Radius * transform.right, transform.position + orbit.Height * transform.up +
                orbit.Radius * transform.right);
        }

        public void Move(Vector2 input)
        {
            height = Mathf.Clamp(height + input.y * pitchSpeed * Time.deltaTime, 
                BottomOrbit.Height, TopOrbit.Height);
            theta = (theta + input.x * yawSpeed * Time.deltaTime) % 360f;

            radius = InterpolateRadius(height);
        }

        float InterpolateRadius(float height)
        {
            return 3f;
        }

        #region InterfaceMethods
        public Vector3 ExpectedCamPosition(Transform follow, Transform target)
        {
            if (Locked)
            {
                var ds = (target.position - follow.position).normalized;
                ds.y = 0;

                theta = -Camera.main.transform.eulerAngles.y;

                return follow.transform.position + height * Vector3.up - radius * ds;
            }

            return follow.transform.position +
                height * Vector3.up + radius * (Mathf.Sin(theta * Mathf.Deg2Rad) *
                Vector3.right - Mathf.Cos(theta * Mathf.Deg2Rad) * Vector3.forward);
        }

        public void LockCamera(bool value)
        {
            Locked = value;

            if (Locked)
            {
                height = TopOrbit.Height;
                radius = TopOrbit.Radius;
            }
        }
        #endregion
    }
}