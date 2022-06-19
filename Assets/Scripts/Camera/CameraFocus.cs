using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CameraFocus : MonoBehaviour
    {
        public float freeHeight = 1f;
        public float lockedHeight = 2f;
        public float updateSpeed = 8f;

        Vector3 pos;

        PartyController party;
        Controller controller;
        TargetSphere targetSphere;

        private void Awake()
        {
            party = GetComponentInParent<PartyController>();
        }

        public void Init(Controller controller)
        {
            pos = transform.localPosition;

            this.controller = controller;
            targetSphere = controller.TargetSphere;
        }

        private void Update()
        {
            var height = 0f;

            if (targetSphere.enabled) height = lockedHeight;
            else height = freeHeight;

            transform.position = Vector3.MoveTowards(transform.position, 
                party.CurrentControllerTransform.position + height * Vector3.up, 
                updateSpeed * Time.deltaTime);

            if (targetSphere.enabled) transform.rotation = controller.transform.rotation;
            else transform.rotation = Quaternion.identity;
        }
    }
}