using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG_Project
{
    public class CharacterModel : MonoBehaviour
    {
        [field: SerializeField] public bool InMotion { get; private set; }
        public bool LockOnRotation { get; private set; }

        [field: SerializeField] public Transform Hips { get; private set; }

        [field: SerializeField] public float FadeTime { get; private set; } = 0.1f;

        float angle;
        float target;
        float turnVelocity;

        Controller controller;
        [field: SerializeField] public Animator Anim { get; private set; }

        PartyController party;

        CharacterController cc;
        CapsuleCollider col;

        TargetSphere targetSphere;

        public Vector3 AbsoluteDir(Vector3 dir) => dir.z * transform.forward + 
            dir.x * transform.right;

        private void Awake()
        {
            party = GetComponentInParent<PartyController>();

            controller = GetComponentInParent<Controller>();
            cc = GetComponentInParent<CharacterController>();

            Anim = GetComponent<Animator>();
        }

        public void Init()
        {
            targetSphere = controller.TargetSphere;
        }

        public void RotateModel(Vector3 dir, float dt)
        {
            if (targetSphere.Active)
                LookAt(targetSphere.CurrentTargetTransform.position);
            else
            {
                if (dir != Vector3.zero)
                {
                    target = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

                    angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        target, ref turnVelocity, 0.1f);
                    Hips.transform.rotation = Quaternion.Euler(0, angle, 0);
                }
            }
        }

        public void LookAt(Vector3 look)
        {
            look.y = Hips.transform.position.y;
            Hips.transform.LookAt(look);
        }

        public void PlayAnimationFade(int stateHash, int layer, bool allowReentry)
        {
            if (!allowReentry)
            {
                if (stateHash != Anim.GetCurrentAnimatorStateInfo(0).shortNameHash)
                    Anim.CrossFadeInFixedTime(stateHash, FadeTime);
            }
            else Anim.CrossFadeInFixedTime(stateHash, FadeTime);
        }

        #region AnimParameters
        public void SetAnimSpeed(float speed)
        {
            Anim.SetFloat("Speed", speed);
        }

        public void SetAnimHorizontal(float horizontal)
        {
            Anim.SetFloat("Horizontal", horizontal);
        }

        public void SetAnimVertical(float vertical)
        {
            Anim.SetFloat("Vertical", vertical);
        }

        public void SetAnimDir(Vector3 dir)
        {
            Anim.SetFloat("Horizontal", dir.x);
            Anim.SetFloat("Vertical", dir.z);
        }
        #endregion

        //public void SetAnimLocked(bool locked)
        //{
        //    anim.SetBool("Locked", locked);
        //}

        //public void SetAnimFalling(bool falling)
        //{
        //    anim.SetBool("Falling", falling);
        //}

        #region AnimationEventMethods
        public void AdvanceAction()
        {
            controller.ActionQueue.AdvanceAction();
        }

        public void ApplyForce(Vector3 force, float dt)
        {
            controller.Movement.ApplyForce(force, dt);
        }

        public void StartMotion()
        {
            InMotion = true;
        }

        public void StopMotion()
        {
            InMotion = false;
        }

        public void EnableLockOnRotation()
        {
            LockOnRotation = true;
        }

        public void DisableLockOnRotation()
        {
            LockOnRotation = false;
        }
        #endregion
    }
}