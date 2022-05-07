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

        [SerializeField] List<BattleAction> actions = new List<BattleAction>();
        int actionCap = 5;

        PartyController party;

        public bool Executing
        {
            get => executing;
            set => executing = value;
        }

        public List<BattleAction> Actions => actions;

        public BattleAction TopAction
        {
            get
            {
                if (actions.Count > 0) return actions[0];
                return null;
            }
        }

        public string TopAnimation => TopAction?.AnimationStateName;

        public Controller CurrentController => party.CurrentController;

        private void Awake()
        {
            party = GetComponent<PartyController>();
        }

        public void AddAction(BattleAction action)
        {
            if (actions.Count < actionCap)
                actions.Add(action);
        }

        public void ClearActions()
        {
            if (actions.Count > 0) actions.Clear();
        }

        public void AdvanceAction()
        {
            if (actions.Count > 0) actions.RemoveAt(0);

            if (actions.Count <= 0) StopChain();
            else
            {
                if (!CurrentController.Movement.Grounded)
                    StopChain();
                else CurrentController.Model.PlayAnimation(TopAction.AnimationStateName, 0);
            }
        }

        void StopChain()
        {
            executing = false;

            if (!CurrentController.Movement.Grounded)
                CurrentController.sm.ChangeState(StateID.ControllerFall);
            else
            {
                if (CurrentController.TargetSphere.enabled)
                    CurrentController.sm.ChangeState(StateID.ControllerStrafe);
                else CurrentController.sm.ChangeState(StateID.ControllerMove);
            }

            actions.Clear();
        }
    }
}