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

        Dictionary<QueueAction, string> actionTriggers = new Dictionary<QueueAction, string>();

        PartyController party;

        public bool Executing
        {
            get => executing;
            set => executing = value;
        }

        public List<QueueAction> Actions => actions;

        public QueueAction TopAction
        {
            get
            {
                if (actions.Count > 0) return actions[0];
                return QueueAction.Unqueuable;
            }
        }

        public string TopTrigger
        {
            get
            {
                if (actionTriggers.ContainsKey(TopAction))
                    return actionTriggers[TopAction];
                return "";
            }
        }

        public Controller CurrentController => party.CurrentController;

        private void Awake()
        {
            party = GetComponent<PartyController>();

            actionTriggers.Add(QueueAction.ActionL1, "ActionL1");
            actionTriggers.Add(QueueAction.ActionL2, "ActionL2");
            actionTriggers.Add(QueueAction.ActionR1, "ActionR1");
            actionTriggers.Add(QueueAction.ActionR2, "ActionR2");
            actionTriggers.Add(QueueAction.Defend, "Defend");
        }

        public void AddAction(QueueAction action)
        {
            if (actions.Count < actionCap)
                actions.Add(action);
        }

        public void AdvanceAction()
        {
            if (actions.Count > 0) actions.RemoveAt(0);

            if (actions.Count <= 0) StopChain();
            else
            {
                if (!CurrentController.Movement.Grounded)
                    StopChain();
                else CurrentController.Model.PlayAnimation(TopTrigger, 0);
            }
        }

        void StopChain()
        {
            executing = false;

            if (!CurrentController.Movement.Grounded)
                CurrentController.sm.ChangeState(StateID.ControllerFall);
            else
            {
                print(2);
                if (party.Pivot.locked)
                    CurrentController.sm.ChangeState(StateID.ControllerStrafe);
                else CurrentController.sm.ChangeState(StateID.ControllerMove);
            }

            actions.Clear();
        }
    }
}