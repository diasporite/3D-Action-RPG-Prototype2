using UnityEngine;

namespace RPG_Project
{
    public interface ICamera
    {
        Vector3 ExpectedCamPosition(Transform follow, Transform target);

        void LockCamera(bool value);
    }
}