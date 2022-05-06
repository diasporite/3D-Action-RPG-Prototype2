using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class InputButton
    {
        [SerializeField] bool value;

        bool hold;

        public bool Input
        {
            get
            {
                if (!hold)
                {
                    var pressed = value;
                    value = false;
                    return pressed;
                }

                return value;
            }
            set => this.value = value;
        }

        public InputButton(bool hold)
        {
            value = false;
            this.hold = hold;
        }
    }
}