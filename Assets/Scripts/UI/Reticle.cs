using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class Reticle : MonoBehaviour
    {
        Image image;
        TargetSphere targetSphere;

        float speed = 90;

        private void Awake()
        {
            image = GetComponent<Image>();
            targetSphere = FindObjectOfType<PlayerInputController>().GetComponentInChildren<TargetSphere>();

            image.enabled = false;
        }

        private void Update()
        {
            if (image.enabled)
            {
                transform.position = Camera.main.WorldToScreenPoint(targetSphere.CurrentTargetTransform.position);
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