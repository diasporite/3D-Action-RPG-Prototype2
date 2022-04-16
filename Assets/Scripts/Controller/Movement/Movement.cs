using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum MovementState
    {
        Walk = 0,
        Run = 1,
        Fall = 2,
    }

    public class Movement : MonoBehaviour
    {
        [Header("Info")]
        [SerializeField] MovementState state;
        public bool grounded;

        [Header("Speed")]
        public float walkSpeed = 4f;
        public float runSpeed = 6f;

        [SerializeField] float currentSpeed;

        [Header("Gravity")]
        [SerializeField] float timeSinceGrounded = 0f;
        [SerializeField] float verticalSpeed = 0f;
        [SerializeField] float terminalVelocity = 40f;
        float gravity = -9.81f;
        public float damageSpeedThreshold = 20f;

        CharacterModel model;
        GroundCheck gc;
        CameraPivot pivot;

        CharacterController cc;
        Vector3 ds;

        public MovementState State
        {
            get => state;
            set
            {
                state = value;

                switch (state)
                {
                    case MovementState.Walk:
                        currentSpeed = walkSpeed;
                        break;
                    case MovementState.Run:
                        currentSpeed = runSpeed;
                        break;
                    default:
                        break;
                }
            }
        }

        private void Update()
        {
            Fall(Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, ds);
            Gizmos.color = Color.red;
            if (model != null)
                Gizmos.DrawRay(transform.position, model.AbsoluteDir(ds));
            Gizmos.color = Color.blue;
            if (model != null)
                Gizmos.DrawRay(transform.position, model.transform.forward);
        }

        public void Init()
        {
            cc = GetComponent<CharacterController>();

            model = GetComponentInChildren<CharacterModel>();
            gc = GetComponentInChildren<GroundCheck>();
            pivot = GetComponentInParent<PartyController>().Pivot;

            State = MovementState.Walk;
        }

        public void MovePosition(Vector3 dir, float dt)
        {
            model.RotateModel(dir, dt);

            cc.Move(currentSpeed * (Quaternion.Euler(0, -pivot.Theta, 0) * dir) * dt);
        }

        public void MovePositionAround(Vector3 dir, float dt)
        {
            var around = pivot.targetPos;
            model.LookAt(pivot.targetPos);

            around.y = transform.position.y;
            transform.LookAt(around);

            cc.Move(currentSpeed * dir * dt);
        }

        public void MovePositionAround(Vector3 dir, Vector3 around, float dt)
        {
            model.LookAt(around);

            around.y = transform.position.y;
            transform.LookAt(around);

            cc.Move(currentSpeed * dir * dt);
        }

        public void Fall(float dt)
        {
            grounded = gc.IsGrounded;

            if (grounded)
            {
                timeSinceGrounded = 0;
                verticalSpeed = 0;
            }
            else
            {
                timeSinceGrounded += dt;
                verticalSpeed = Mathf.Clamp(verticalSpeed + gravity * dt, 
                    -terminalVelocity, terminalVelocity);
                cc.Move(verticalSpeed * transform.up * dt);
            }
        }
    }
}