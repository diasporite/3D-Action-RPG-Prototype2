using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class HoldInput : InputButton
    {
        public override bool GetInput => Input.GetKey(key);

        public HoldInput(string key) : base(key)
        {

        }
    }
}