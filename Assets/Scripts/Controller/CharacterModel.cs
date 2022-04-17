using System.Collections;
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

        Animator anim;

        Controller controller;

        CharacterController cc;

        public Vector3 AbsoluteDir(Vector3 dir) => dir.z * transform.forward + 
            dir.x * transform.right;

        private void Awake()
        {
            anim = GetComponent<Animator>();

            controller = GetComponentInParent<Controller>();

            cc = GetComponentInParent<CharacterController>();
        }

        public void RotateModel(Vector3 dir, float dt)
        {
            if (controller.Pivot.locked)
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
                }
            }
        }

        public void LookAt(Vector3 look)
        {
            look.y = transform.position.y;
            transform.LookAt(look);
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

        public void SetAnimLocked(bool locked)
        {
            anim.SetBool("Locked", locked);
        }

        public void SetAnimFalling(bool falling)
        {
            anim.SetBool("Falling", falling);
        }
    }
}