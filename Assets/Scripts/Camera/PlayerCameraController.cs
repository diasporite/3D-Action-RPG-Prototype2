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

        CinemachineFreeLook freeLook;
        [SerializeField] CinemachineVirtualCamera vcam;
        CinemachineTargetGroup targetGroup;

        private void Awake()
        {
            party = GetComponent<PartyController>();
            input = GetComponent<PlayerInputController>();

            freeLook = GetComponentInChildren<CinemachineFreeLook>();
            targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
        }

        private void OnEnable()
        {
            input.LockAction += ToggleCam;
        }

        private void OnDisable()
        {
            input.LockAction -= ToggleCam;
        }

        void ToggleCam()
        {
            Locked = !Locked;

            freeLook.gameObject.SetActive(!Locked);
            vcam.gameObject.SetActive(Locked);
        }
    }
}