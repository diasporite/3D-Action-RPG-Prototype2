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
        [SerializeField] bool grounded;

        [Header("Speed")]
        public float walkSpeed = 4f;
        public float runSpeed = 6f;
        [SerializeField] float currentSpeed;

        [Header("Forces")]
        [SerializeField] Vector3 forceVelocity = 
            new Vector3(0, 0, 0);
        [field: SerializeField] public float DragTime { get; private set; }

        [Header("Gravity")]
        [SerializeField] float timeSinceGrounded = 0f;
        [SerializeField] float terminalVelocity = 40f;
        float gravity = -9.81f;
        public float damageSpeedThreshold = 20f;

        Controller controller;
        CharacterModel model;
        GroundCheck gc;
        TargetSphere targetSphere;

        CharacterController cc;

        public Vector3 dir;
        public Vector3 ds;
        Vector3 dampingVelocity;

        Transform mainCamTransform;

        float target = 0f;
        float angle = 0f;
        float turnVelocity = 0f;

        float dropSpeed;

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

        public bool Grounded => grounded;

        public Vector3 Move(Vector3 dir) => forceVelocity + (currentSpeed * dir);

        public Vector3 Move(float speed, Vector3 dir) => forceVelocity + (speed * dir);

        public Vector3 ForceVelocity { set => forceVelocity = value; }

        private void Update()
        {
            Fall(Time.deltaTime);
        }

        public void Init()
        {
            controller = GetComponent<Controller>();
            cc = GetComponent<CharacterController>();

            model = GetComponentInChildren<CharacterModel>();
            gc = GetComponentInChildren<GroundCheck>();
            targetSphere = controller.TargetSphere;

            mainCamTransform = Camera.main.transform;

            State = MovementState.Walk;

            dropSpeed = -Mathf.Tan(cc.slopeLimit * Mathf.Deg2Rad);
        }

        public void MovePositionFree(Vector3 dir, float dt, bool damping)
        {
            if (dir != transform.forward) RotateModel(dir, dt);

            model.SetAnimSpeed(dir.magnitude * currentSpeed);
            model.SetAnimDir(dir);

            if (dir != Vector3.zero)
                cc.Move(Move(transform.forward) * dt);
        }

        public void MovePositionFree(float speed, Vector3 dir, float dt, bool damping)
        {
            if (dir != transform.forward) RotateModel(dir, dt);

            model.SetAnimSpeed(dir.magnitude * currentSpeed);
            model.SetAnimDir(dir);

            if (dir != Vector3.zero)
                cc.Move(Move(speed, transform.forward) * dt);
        }

        public void MovePositionStrafe(Vector3 dir, float dt, bool damping)
        {
            LookAt(targetSphere.CurrentTargetTransform.position);

            model.SetAnimSpeed(dir.magnitude * currentSpeed);
            model.SetAnimDir(dir);

            var ds = transform.forward * dir.z + transform.right * dir.x;

            if (dir != Vector3.zero)
                cc.Move(Move(ds) * dt);
        }

        public void MovePositionStrafe(float speed, Vector3 dir, float dt, bool damping)
        {
            LookAt(targetSphere.CurrentTargetTransform.position);

            model.SetAnimSpeed(dir.magnitude * currentSpeed);
            model.SetAnimDir(dir);

            var ds = transform.forward * dir.z + transform.right * dir.x;

            if (dir != Vector3.zero)
                cc.Move(Move(speed, ds) * dt);
        }

        public void Fall(float dt)
        {
            grounded = gc.IsGrounded;

            if (grounded)
            {
                timeSinceGrounded = 0;
                forceVelocity.y = 0;

                cc.Move(gravity * transform.up * dt);
            }
            else
            {
                timeSinceGrounded += dt;
                forceVelocity.y = Mathf.Clamp(forceVelocity.y + gravity * dt, 
                    -terminalVelocity, terminalVelocity);
                cc.Move(forceVelocity * dt);
            }
        }

        public void UpdateForce()
        {
            forceVelocity = Vector3.SmoothDamp(forceVelocity, Vector3.zero,
                ref dampingVelocity, DragTime);
        }

        public void RotateModel(Vector3 dir, float dt)
        {
            if (targetSphere.enabled)
                LookAt(targetSphere.CurrentTargetTransform.position);
            else
            {
                if (dir != Vector3.zero)
                {
                    target = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                    if (controller.IsPlayer) target += mainCamTransform.eulerAngles.y;

                    angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        target, ref turnVelocity, 0.1f);
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                }
            }
        }

        public void LookAt(Vector3 look)
        {
            look.y = transform.position.y;
            transform.LookAt(look);
        }

        public void LookTowards(Vector3 look)
        {
            look.y = 0;
            Quaternion.LookRotation(look);
        }
    }
}