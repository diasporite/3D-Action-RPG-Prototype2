using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    // Strafing is separate from mode
    public enum MovementState
    {
        ThirdPerson = 0,
        TopDown = 1,
        SideScroll = 2,
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

        [Header("Forces")]
        [SerializeField] Vector3 forceVelocity = 
            new Vector3(0, 0, 0);
        [field: SerializeField] public float DragTime { get; private set; }

        [Header("Gravity")]
        [SerializeField] float timeSinceGrounded = 0f;
        [SerializeField] float verticalVelocity = 0f;
        [SerializeField] float terminalVelocity = 40f;
        float gravity = -9.81f;
        [SerializeField] float damageSpeedThreshold = 20f;
        [SerializeField] float fallDamageScaling = 0.05f;

        PartyController party;
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
        float turnTime = 0.1f;

        float dropSpeed;

        public bool Grounded => grounded;

        public Vector3 Move(float speed, Vector3 dir) => forceVelocity + (speed * dir);

        public Vector3 ForceVelocity { set => forceVelocity = value; }

        private void Awake()
        {
            party = GetComponentInParent<PartyController>();
        }

        private void Update()
        {
            UpdateForce();

            Fall(Time.deltaTime);
        }

        public void Init()
        {
            controller = GetComponent<Controller>();
            cc = GetComponentInParent<CharacterController>();

            model = GetComponent<CharacterModel>();
            gc = GetComponentInParent<PartyController>().GetComponentInChildren<GroundCheck>();
            targetSphere = controller.TargetSphere;

            mainCamTransform = Camera.main.transform;

            dropSpeed = -Mathf.Tan(cc.slopeLimit * Mathf.Deg2Rad);
        }

        public void MovePositionFree(Vector3 dir, float dt)
        {
            RotateModel(dir, dt);

            model?.SetAnimSpeed(dir.magnitude * WalkSpeed);
            model?.SetAnimDir(dir);

            if (dir != Vector3.zero)
                cc.Move(Move(WalkSpeed, transform.forward) * dt);
        }

        public void MovePositionFree(float speed, Vector3 dir, float dt)
        {
            RotateModel(dir, dt);

            model?.SetAnimSpeed(dir.magnitude * speed);
            model?.SetAnimDir(dir);

            if (dir != Vector3.zero)
                cc.Move(Move(speed, transform.forward) * dt);
        }

        public void MovePositionRun(Vector3 dir, float dt)
        {
            RotateModel(dir, dt);

            model?.SetAnimSpeed(dir.magnitude * RunSpeed);
            model?.SetAnimDir(dir);

            if (dir != Vector3.zero)
                cc.Move(Move(RunSpeed, transform.forward) * dt);
        }

        public void MovePositionStrafe(Vector3 dir, float dt)
        {
            RotateTowards(party.transform, targetSphere.CurrentTargetTransform);

            model?.SetAnimSpeed(dir.magnitude * StrafeSpeed);
            model?.SetAnimDir(dir);

            var ds = transform.forward * dir.z + transform.right * dir.x;

            if (dir != Vector3.zero)
                cc.Move(Move(StrafeSpeed, ds) * dt);
        }

        public void MovePositionStrafe(float speed, Vector3 dir, float dt)
        {
            RotateTowards(party.transform, targetSphere.CurrentTargetTransform);

            model?.SetAnimSpeed(dir.magnitude * speed);
            model?.SetAnimDir(dir);

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
                if (Mathf.Abs(verticalVelocity) > Mathf.Abs(damageSpeedThreshold))
                    ApplyFallDamage();

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
                RotateTowards(party.transform, targetSphere.CurrentTargetTransform);
            else
            {
                if (dir != Vector3.zero)
                {
                    target = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                    if (controller.IsPlayer) target += mainCamTransform.eulerAngles.y;

                    angle = Mathf.SmoothDampAngle(party.transform.eulerAngles.y,
                        target, ref turnVelocity, turnTime);
                    party.transform.rotation = Quaternion.Euler(0, angle, 0);
                }
            }
        }

        public void RotateTowards(Transform t, Transform target)
        {
            var ds = target.position - transform.position;
            ds.y = 0;

            t.rotation = Quaternion.LookRotation(Vector3.MoveTowards(transform.forward, ds, 10f));
        }

        void ApplyFallDamage()
        {
            var dv = Mathf.Abs(verticalVelocity) - damageSpeedThreshold;
            var damage = Mathf.RoundToInt(controller.Party.Health.ResourceStatValue *
                dv * fallDamageScaling);

            controller.Combatant.OnDamage(new DamageInfo(controller.Combatant, damage, 0));
        }
    }
}