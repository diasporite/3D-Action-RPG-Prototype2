using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CameraController : MonoBehaviour
    {
        public float updateSpeed = 40f;

        public CameraPivot pivot;

        private void Start()
        {
            transform.position = pivot.transform.position;
            transform.rotation = pivot.transform.rotation;
        }

        private void LateUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                pivot.transform.position, updateSpeed * Time.deltaTime);

            transform.LookAt(pivot.targetPos);
        }
    }
}