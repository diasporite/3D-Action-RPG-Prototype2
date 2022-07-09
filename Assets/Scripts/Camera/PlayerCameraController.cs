using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG_Project
{
    public class PlayerCameraController : MonoBehaviour
    {
        [field: SerializeField] public bool Locked { get; private set; }

        PartyController party;
        InputController input;
        TargetSphere targetSphere;

        //CinemachineFreeLook freeLook;
        //[SerializeField] CinemachineVirtualCamera vcam;
        //CinemachineTargetGroup targetGroup;

        CameraController cc;
        ThirdPersonCamera thirdPerson;

        public ICamera CurrentCamera { get; private set; }

        private void Awake()
        {
            party = GetComponent<PartyController>();
            input = GetComponent<PlayerInputController>();
            targetSphere = party.GetComponentInChildren<TargetSphere>();

            //freeLook = GetComponentInChildren<CinemachineFreeLook>();
            //targetGroup = GetComponentInChildren<CinemachineTargetGroup>();

            cc = Camera.main.GetComponent<CameraController>();
            thirdPerson = GetComponentInChildren<ThirdPersonCamera>();
        }

        private void Start()
        {
            CurrentCamera = thirdPerson;
        }

        private void OnEnable()
        {
            targetSphere.OnLockOn += LockOnCam;
            targetSphere.OnLockOff += LockOffCam;
        }

        private void OnDisable()
        {
            targetSphere.OnLockOn -= LockOnCam;
            targetSphere.OnLockOff -= LockOffCam;
        }

        void LockOnCam()
        {
            Locked = true;

            //freeLook.gameObject.SetActive(!Locked);
            //vcam.gameObject.SetActive(Locked);

            CurrentCamera.LockCamera(Locked);
        }

        void LockOffCam()
        {
            Locked = false;

            //freeLook.gameObject.SetActive(!Locked);
            //vcam.gameObject.SetActive(Locked);

            CurrentCamera.LockCamera(Locked);
        }
    }
}