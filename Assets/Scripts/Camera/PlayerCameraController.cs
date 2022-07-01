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

        CinemachineFreeLook freeLook;
        [SerializeField] CinemachineVirtualCamera vcam;
        CinemachineTargetGroup targetGroup;

        private void Awake()
        {
            party = GetComponent<PartyController>();
            input = GetComponent<PlayerInputController>();
            targetSphere = party.GetComponentInChildren<TargetSphere>();

            freeLook = GetComponentInChildren<CinemachineFreeLook>();
            targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
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

            freeLook.gameObject.SetActive(!Locked);
            vcam.gameObject.SetActive(Locked);
        }

        void LockOffCam()
        {
            Locked = false;

            freeLook.gameObject.SetActive(!Locked);
            vcam.gameObject.SetActive(Locked);
        }
    }
}