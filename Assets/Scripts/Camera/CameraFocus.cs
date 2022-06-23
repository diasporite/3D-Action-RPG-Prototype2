using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG_Project
{
    public class CameraFocus : MonoBehaviour
    {
        [field: SerializeField]
        public Vector3 freeOffset { get; private set; } = 
            new Vector3(0f, 0.5f, -1f);

        [field: SerializeField]
        public Vector3 lockedOffset { get; private set; } =
            new Vector3(0f, 0.5f, -1f);

        public float updateSpeed = 50f;

        Vector3 offset;
        Vector3 pos;
        Vector3 newPos;

        PartyController party;
        Controller controller;
        TargetSphere targetSphere;
        Transform controllerTransform;

        CinemachineFreeLook freeLook;
        CinemachineVirtualCamera vcam;
        CinemachineTargetGroup targetGroup;

        private void Awake()
        {
            party = GetComponentInParent<PartyController>();
        }

        private void OnEnable()
        {
            party.OnCharacterChanged += UpdateCameras;
        }

        private void OnDisable()
        {
            party.OnCharacterChanged -= UpdateCameras;
        }

        public void Init()
        {
            pos = transform.localPosition + freeOffset;

            targetSphere = party.TargetSphere;

            freeLook = party.GetComponentInChildren<CinemachineFreeLook>();
            vcam = party.GetComponentInChildren<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (party.CurrentControllerTransform != controllerTransform)
                controllerTransform = party.CurrentControllerTransform;

            if (targetSphere.Active) offset = lockedOffset;
            else offset = freeOffset;

            transform.position = controllerTransform.position +
                offset.y * controllerTransform.up + offset.z * controllerTransform.forward;

            if (targetSphere.Active) transform.rotation = controllerTransform.rotation;
            else transform.rotation = Quaternion.identity;
        }

        void UpdateCameras()
        {

        }
    }
}