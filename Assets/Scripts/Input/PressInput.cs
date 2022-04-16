using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class PressInput : InputButton
    {
        [SerializeField] QueueAction queueAction;

        public override bool GetInput => Input.GetKeyDown(key);

        public QueueAction QueueAction;

        public PressInput(string key) : base(key)
        {
            queueAction = QueueAction.Unqueuable;
        }

        public PressInput(string key, QueueAction queueAction) : base(key)
        {
            this.queueAction = queueAction;
        }
    }
}