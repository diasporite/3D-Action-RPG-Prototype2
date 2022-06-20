using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG_Project
{
    public class PlayerCameraController : MonoBehaviour
    {
        CinemachineStateDrivenCamera stateCam;
        CinemachineFreeLook freeLook;
        CinemachineVirtualCamera vcam;

        PartyController party;

        private void Awake()
        {
            stateCam = GetComponentInChildren<CinemachineStateDrivenCamera>();
            freeLook = GetComponentInChildren<CinemachineFreeLook>();
            vcam = GetComponentInChildren<CinemachineVirtualCamera>();

            party = GetComponent<PartyController>();
        }
    }
}