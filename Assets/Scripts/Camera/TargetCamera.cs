using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG_Project
{
    // Custom targeting camera
    public class TargetCamera : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera vcam;

        private void Awake()
        {
            vcam = GetComponent<CinemachineVirtualCamera>();
        }
    }
}