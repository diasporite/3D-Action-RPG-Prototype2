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

        [field: Header("Speed")]
        [field: SerializeField] public float WalkSpeed { get; private set; } = 4f;
        [field: SerializeField] public float RunSpeed { get; private set; } = 6f;
        [field: SerializeField] public float StrafeSpeed { get; private set; } = 3f;
        [SerializeField] float currentSpeed;

        [Header("Forces")]
        [SerializeField] Vector3 forceVelocity = 
            new Vector3(0, 0, 0);
        [field: SerializeField] public float DragTime { get; private set; }

        [Header("Gravity")]
        [SerializeField] float timeSinceGrounded = 0f;
        [SerializeField] float verticalVelocity = 0f;
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
                        currentSpeed = WalkSpeed;
                        break;
                    case MovementState.Run:
                        currentSpeed = RunSpeed;
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
            RotateModel(dir, dt);

            model.SetAnimSpeed(dir.magnitude * currentSpeed);
            model.SetAnimDir(dir);

            if (dir != Vector3.zero)
                cc.Move(Move(transform.forward) * dt);
        }

        public void MovePositionFree(float speed, Vector3 dir, float dt, bool damping)
        {
            RotateModel(dir, dt);

            model.SetAnimSpeed(dir.magnitude * currentSpeed);
            model.SetAnimDir(dir);

            if (dir != Vector3.zero)
                cc.Move(Move(speed, transform.forward) * dt);
        }

        public void MovePositionStrafe(Vector3 dir, float dt, bool damping)
        {
            RotateTowards(targetSphere.CurrentTargetTransform);

            model.SetAnimSpeed(dir.magnitude * currentSpeed);
            model.SetAnimDir(dir);

            var ds = transform.forward * dir.z + transform.right * dir.x;

            if (dir != Vector3.zero)
                cc.Move(Move(ds) * dt);
        }

        public void MovePositionStrafe(float speed, Vector3 dir, float dt, bool damping)
        {
            RotateTowards(targetSphere.CurrentTargetTransform);

            model.SetAnimSpeed(dir.magnitude * currentSpeed);
            model.SetAnimDir(dir);

            var ds = transform.forward * dir.z + transform.right * dir.x;

            if (dir != Vector3.zero)
                cc.Move(Move(speed, ds) * dt);
        }

        public void MovePositionForward(float speed, float dt, bool damping)
        {
            cc.Move(Move(speed, transform.forward) * dt);
        }

        public void MoveTo(Vector3 pos)
        {
            cc.Move(pos - transform.position);
        }

        public void Fall(float dt)
        {
            grounded = gc.IsGrounded;

            if (grounded)
            {
                timeSinceGrounded = 0;
                verticalVelocity = 0;

                cc.Move(gravity * transform.up * dt);
            }
            else
            {
                timeSinceGrounded += dt;
                verticalVelocity = Mathf.Clamp(verticalVelocity + gravity * dt, 
                    -terminalVelocity, terminalVelocity);
                cc.Move(verticalVelocity * transform.up * dt);
            }
        }

        public void UpdateForce()
        {
            forceVelocity = Vector3.SmoothDamp(forceVelocity, Vector3.zero,
                ref dampingVelocity, DragTime);
        }

        public void RotateModel(Vector3 dir, float dt)
        {
            if (targetSphere.Active)
                RotateTowards(targetSphere.CurrentTargetTransform);
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

        public void RotateTowards(Transform target)
        {
            var ds = target.position - transform.position;
            ds.y = 0;

            transform.rotation = Quaternion.LookRotation(Vector3.MoveTowards(transform.forward, ds, 10f));
        }
    }
}