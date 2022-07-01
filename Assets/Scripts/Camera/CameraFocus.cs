using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private void Awake()
        {
            party = GetComponentInParent<PartyController>();
        }

        private void Start()
        {
            Init();
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
            pos = transform.localPosition;

            targetSphere = party.TargetSphere;
        }

        private void Update()
        {
            if (targetSphere.Active)
            {
                transform.localPosition = lockedOffset;
                transform.rotation = party.transform.rotation;
            }
            else
            {
                transform.position = party.transform.position + freeOffset;
                transform.rotation = Quaternion.identity;
            }
        }

        void UpdateCameras()
        {

        }
    }
}