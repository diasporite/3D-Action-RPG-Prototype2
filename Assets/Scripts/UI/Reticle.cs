using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class Reticle : MonoBehaviour
    {
        Camera mainCam;

        Image image;
        TargetSphere targetSphere;

        [SerializeField] float speed = 90;

        private void Awake()
        {
            mainCam = Camera.main;

            image = GetComponent<Image>();
            targetSphere = FindObjectOfType<PlayerInputController>().GetComponentInChildren<TargetSphere>();

            image.enabled = false;
        }

        private void Update()
        {
            if (image.enabled)
            {
                transform.position = mainCam.WorldToScreenPoint(targetSphere.CurrentTargetTransform.position);
                transform.Rotate(0, 0, speed * Time.deltaTime);
            }
        }

        private void OnEnable()
        {
            targetSphere.OnLockOn += ShowReticle;
            targetSphere.OnLockOff += HideReticle;
        }

        private void OnDisable()
        {
            targetSphere.OnLockOn -= ShowReticle;
            targetSphere.OnLockOff -= HideReticle;
        }

        void ShowReticle()
        {
            image.enabled = true;
        }

        void HideReticle()
        {
            image.enabled = false;
        }
    }
}