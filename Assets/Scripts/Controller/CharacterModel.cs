using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CharacterModel : MonoBehaviour
    {
        [field: SerializeField] public bool InMotion { get; private set; }
        public bool LockOnRotation { get; private set; }

        [SerializeField] GameObject charModel;
        [SerializeField] CameraFocus focus;

        [field: SerializeField] public float WalkSpeed { get; private set; } = 3.5f;
        [field: SerializeField] public float RunSpeed { get; private set; } = 7f;
        [field: SerializeField] public float StrafeSpeed { get; private set; } = 5f;
        [field: SerializeField] public float RollSpeed { get; private set; } = 3f;

        [SerializeField] float fadeTime = 0.1f;

        float angle;
        float target;
        float turnVelocity;

        Controller controller;
        [field: SerializeField] public Animator Anim { get; private set; }

        CharacterController cc;
        CapsuleCollider col;

        TargetSphere targetSphere;

        public Vector3 AbsoluteDir(Vector3 dir) => dir.z * transform.forward + 
            dir.x * transform.right;

        private void Awake()
        {
            controller = GetComponentInParent<Controller>();
            cc = GetComponentInParent<CharacterController>();

            Anim = GetComponent<Animator>();
        }

        public void Init()
        {
            focus.Init(controller);

            targetSphere = controller.TargetSphere;
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

                    angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        target, ref turnVelocity, 0.1f);
                    charModel.transform.rotation = Quaternion.Euler(0, angle, 0);
                }
            }
        }

        public void LookAt(Vector3 look)
        {
            look.y = charModel.transform.position.y;
            charModel.transform.LookAt(look);
        }

        public void PlayAnimation(string stateName, int layer)
        {
            Anim.Play(stateName, 0);
        }

        public void PlayAnimation(int stateHash, int layer)
        {
            Anim.Play(stateHash, 0);
        }

        public void PlayAnimationFade(string stateName, int layer, float dt)
        {
            Anim.CrossFade(stateName, dt);
        }

        public void PlayAnimationFade(string stateName, int layer)
        {
            Anim.CrossFade(stateName, fadeTime);
        }

        public void PlayAnimationFade(int stateHash, int layer, float dt)
        {
            Anim.CrossFade(stateHash, dt);
        }

        public void PlayAnimationFade(int stateHash, int layer)
        {
            Anim.CrossFade(stateHash, fadeTime);
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

        public void ApplyForce(Vector3 force)
        {
            controller.Movement.ForceVelocity = force;
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