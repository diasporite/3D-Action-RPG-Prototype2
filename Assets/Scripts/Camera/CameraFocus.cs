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

        Controller controller;
        TargetSphere targetSphere;

        public void Init(Controller controller)
        {
            pos = transform.localPosition;

            this.controller = controller;
            targetSphere = controller.TargetSphere;
        }

        private void Update()
        {
            //transform.position = controller.transform.position + pos;
            transform.position = Vector3.MoveTowards(transform.position, 
                controller.transform.position, updateSpeed * Time.deltaTime);

            if (targetSphere.enabled) transform.rotation = controller.transform.rotation;
            //else transform.rotation = Quaternion.identity;
        }
    }
}