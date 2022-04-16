using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class DirectionInput : InputButton
    {
        [SerializeField] Vector2 dir = new Vector2(0, 0);

        public override bool GetInput => Dir != Vector2.zero;

        public Vector2 Dir
        {
            get
            {
                dir.x = Input.GetAxisRaw("Horizontal");
                dir.y = Input.GetAxisRaw("Vertical");

                return dir;
            }
        }

        public Vector3 DirXz => new Vector3(Dir.x, 0, Dir.y);

        public DirectionInput() : base("")
        {

        }
    }
}