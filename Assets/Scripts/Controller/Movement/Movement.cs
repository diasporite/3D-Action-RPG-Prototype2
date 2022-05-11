﻿using System.Collections;
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

        [Header("Gravity")]
        [SerializeField] float timeSinceGrounded = 0f;
        [SerializeField] float verticalSpeed = 0f;
        [SerializeField] float terminalVelocity = 40f;
        float gravity = -9.81f;
        public float damageSpeedThreshold = 20f;

        Controller controller;
        CharacterModel model;
        GroundCheck gc;
        CameraPivot pivot;

        CharacterController cc;

        public Vector3 dir;
        public Vector3 ds;

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
            pivot = GetComponentInParent<PartyController>().Pivot;

            mainCamTransform = Camera.main.transform;

            State = MovementState.Walk;

            dropSpeed = -Mathf.Tan(cc.slopeLimit * Mathf.Deg2Rad);
        }

        public void MovePositionFree(Vector3 dir, float dt)
        {
            RotateModel(dir, dt);

            model.SetAnimSpeed(dir.magnitude * currentSpeed);
            model.SetAnimDir(dir);

            if (dir != Vector3.zero)
                cc.Move(currentSpeed * transform.forward * dt);
        }

        public void MovePositionStrafe(Vector3 dir, float dt)
        {
            LookAt(controller.TargetSphere.CurrentTargetTransform.position);

            model.SetAnimSpeed(dir.magnitude * currentSpeed);
            model.SetAnimDir(dir);

            var ds = transform.forward * dir.z + transform.right * dir.x;

            if (dir != Vector3.zero)
                cc.Move(currentSpeed * ds * dt);
        }

        public void Fall(float dt)
        {
            grounded = gc.IsGrounded;

            if (grounded)
            {
                if (controller.CurrentState == StateID.ControllerFall)
                    controller.sm.ChangeState(StateID.ControllerMove);

                timeSinceGrounded = 0;
                verticalSpeed = 0;

                cc.Move(gravity * transform.up * dt);
            }
            else
            {
                if (controller.CurrentState != StateID.ControllerFall)
                    controller.sm.ChangeState(StateID.ControllerFall);

                timeSinceGrounded += dt;
                verticalSpeed = Mathf.Clamp(verticalSpeed + gravity * dt, 
                    -terminalVelocity, terminalVelocity);
                cc.Move(verticalSpeed * transform.up * dt);
            }
        }

        public void RotateModel(Vector3 dir, float dt)
        {
            if (controller.TargetSphere.enabled)
                LookAt(controller.Pivot.targetPos);
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
    }
}