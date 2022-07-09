using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class CameraController : MonoBehaviour
    {
        [Range(10f, 100f)]
        [SerializeField] float updateSpeed = 40f;

        PlayerCameraController pcc;
        TargetSphere ts;

        [SerializeField] Transform follow;
        [SerializeField] Transform lookAt;

        private void Awake()
        {
            pcc = FindObjectOfType<PlayerCameraController>();
            ts = pcc.GetComponentInChildren<TargetSphere>();
            follow = pcc.transform;
        }

        private void LateUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position,
                pcc.CurrentCamera.ExpectedCamPosition(follow, ts.TargetFocus),
                updateSpeed * Time.deltaTime);

            //transform.position = Vector3.Lerp(transform.position,
            //    pcc.CurrentCamera.ExpectedCamPosition(follow, ts.TargetFocus), 0.8f);

            //transform.rotation = Quaternion.Lerp(transform.rotation, 
            //    Quaternion.LookRotation(lookAt.position-transform.position), 
            //    0.5f * updateSpeed * Time.deltaTime);

            transform.LookAt(lookAt);
        }
    }
}