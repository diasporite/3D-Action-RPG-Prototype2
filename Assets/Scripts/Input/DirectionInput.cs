using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class DirectionInput : InputButton
    {
        [SerializeField] Vector2 dir = new Vector2(0, 0);

        Vector3 dir_xz = new Vector3(0, 0, 0);

        public override bool GetInput => Dir != Vector2.zero;

        public Vector2 Dir
        {
            get
            {
                dir.x = Input.GetAxis("Horizontal");
                dir.y = Input.GetAxis("Vertical");

                return dir;
            }
        }

        public Vector3 DirXz
        {
            get
            {
                dir_xz.x = Dir.x;
                dir_xz.z = Dir.y;
                if (dir_xz.sqrMagnitude > 1) dir_xz.Normalize();
                return dir_xz;
            }
        }

        public DirectionInput() : base("")
        {

        }
    }
}