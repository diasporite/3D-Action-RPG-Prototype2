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

        CinemachineStateDrivenCamera stateCam;
        CinemachineFreeLook freeLook;
        [SerializeField] CinemachineVirtualCamera vcam;
        CinemachineTargetGroup targetGroup;

        private void Awake()
        {
            party = GetComponent<PartyController>();

            stateCam = GetComponentInChildren<CinemachineStateDrivenCamera>();
            freeLook = GetComponentInChildren<CinemachineFreeLook>();
            targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
        }

        private void OnEnable()
        {
            party.OnCharacterChanged += ChangeAnimatedTarget;
            party.InputController.LockAction += ToggleCam;
        }

        private void OnDisable()
        {
            party.OnCharacterChanged -= ChangeAnimatedTarget;
            party.InputController.LockAction -= ToggleCam;
        }

        void ChangeAnimatedTarget()
        {
            stateCam.m_AnimatedTarget = party.CurrentController.Model.Anim;
        }

        void ToggleCam()
        {
            Locked = !Locked;

            freeLook.gameObject.SetActive(!Locked);
            vcam.gameObject.SetActive(Locked);
        }
    }
}