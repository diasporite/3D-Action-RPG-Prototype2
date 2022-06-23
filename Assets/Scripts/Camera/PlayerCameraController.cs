using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG_Project
{
    public class PlayerCameraController : MonoBehaviour
    {
        PartyController party;

        CinemachineStateDrivenCamera stateCam;
        CinemachineTargetGroup targetGroup;

        private void Awake()
        {
            party = GetComponent<PartyController>();

            stateCam = GetComponentInChildren<CinemachineStateDrivenCamera>();
            targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
        }

        private void OnEnable()
        {
            party.OnCharacterChanged += ChangeAnimatedTarget;
        }

        private void OnDisable()
        {
            party.OnCharacterChanged -= ChangeAnimatedTarget;
        }

        void ChangeAnimatedTarget()
        {
            stateCam.m_LookAt = party.CurrentController.Model.CameraView;
            stateCam.m_AnimatedTarget = party.CurrentController.Model.Anim;
        }
    }
}