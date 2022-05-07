﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CharacterModel : MonoBehaviour
    {
        public bool isPlayer;

        float angle;
        float target;
        float turnVelocity;

        Controller controller;
        Animator anim;

        CharacterController cc;
        CapsuleCollider col;

        public Vector3 AbsoluteDir(Vector3 dir) => dir.z * transform.forward + 
            dir.x * transform.right;

        private void Awake()
        {
            controller = GetComponentInParent<Controller>();
            anim = GetComponentInParent<Animator>();

            cc = GetComponentInParent<CharacterController>();

            col = GetComponent<CapsuleCollider>();

            col.height = cc.height;
            col.radius = cc.radius;
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
                    target -= controller.Party.Pivot.Theta;

                    angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        target, ref turnVelocity, 0.1f);
                    transform.rotation = Quaternion.Euler(0, angle, 0);
                    print(angle + " " + transform.eulerAngles);
                }
            }
        }

        public void LookAt(Vector3 look)
        {
            look.y = transform.position.y;
            transform.LookAt(look);
        }

        public void PlayAnimation(string stateName, int layer)
        {
            print(stateName);
            anim.Play(stateName, 0);
        }

        public void PlayAnimationFade(string stateName, int layer, float dt)
        {
            anim.CrossFade(stateName, dt);
        }

        public void AdvanceAction()
        {
            controller.Party.ActionQueue.AdvanceAction();
        }

        public void SetAnimSpeed(float speed)
        {
            anim.SetFloat("Speed", speed);
        }

        public void SetAnimHorizontal(float horizontal)
        {
            anim.SetFloat("Horizontal", horizontal);
        }

        public void SetAnimVertical(float vertical)
        {
            anim.SetFloat("Vertical", vertical);
        }

        public void SetAnimDir(Vector3 dir)
        {
            anim.SetFloat("Horizontal", dir.x);
            anim.SetFloat("Vertical", dir.z);
        }

        //public void SetAnimLocked(bool locked)
        //{
        //    anim.SetBool("Locked", locked);
        //}

        //public void SetAnimFalling(bool falling)
        //{
        //    anim.SetBool("Falling", falling);
        //}
    }
}