using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class InputButton
    {
        public string key = "";

        public virtual bool GetInput { get; }

        public InputButton(string key)
        {
            this.key = key;
        }
    }
}