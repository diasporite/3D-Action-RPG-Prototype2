using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum QueueAction
    {
        Unqueuable = 0,

        Defend = 1,

        ActionL1 = 5,
        ActionL2 = 6,
        ActionR1 = 7,
        ActionR2 = 8,

        Char1 = 10,
        Char2 = 11,
        Char3 = 12,
        Char4 = 13,
    }

    public class ActionQueue : MonoBehaviour
    {
        [SerializeField] bool executing = false;

        [SerializeField] List<QueueAction> actions = new List<QueueAction>();
        int actionCap = 5;

        public bool Executing
        {
            get => executing;
            set => executing = value;
        }

        public List<QueueAction> Actions => actions;

        public void AddAction(QueueAction action)
        {
            if (actions.Count < actionCap)
                actions.Add(action);
        }

        public void AdvanceAction()
        {
            actions.RemoveAt(0);

            if (actions.Count <= 0) executing = false;
        }
    }
}