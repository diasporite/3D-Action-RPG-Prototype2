using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GroundCheck : MonoBehaviour
    {
        public float radius = 0.02f;
        public float offset = 0.09f;

        float slopeLimit;

        CharacterController cc;
        public LayerMask ground;

        public bool IsGrounded { get => Physics.CheckSphere(transform.position, 
            radius, ground); }

        public float GroundAngle
        {
            get
            {
                RaycastHit[] hit = Physics.RaycastAll(transform.position,
                    -cc.transform.up, -1f, ground);

                return Vector3.Angle(-cc.transform.up, hit[0].normal);
            }
        }

        private void Awake()
        {
            cc = GetComponentInParent<CharacterController>();

            slopeLimit = cc.slopeLimit;

            radius = cc.radius;

            transform.localPosition = -(0.5f * cc.height + offset) * transform.up;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}