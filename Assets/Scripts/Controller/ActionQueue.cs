using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ActionQueue : MonoBehaviour
    {
        [field: SerializeField] public bool Executing { get; private set; }

        [SerializeField] List<BattleAction> actions = new List<BattleAction>();
        int actionCap = 5;

        PartyController party;

        public List<BattleAction> Actions => actions;

        public BattleAction TopAction
        {
            get
            {
                if (actions.Count > 0) return actions[0];
                return null;
            }
        }

        public int TopAnimation => TopAction.AnimStateHash;

        public Controller CurrentController => party.CurrentController;

        private void Awake()
        {
            party = GetComponent<PartyController>();
        }

        public void AddAction(BattleAction action)
        {
            if (actions.Count < actionCap)
                actions.Add(action);

            if (!Executing) StartChain();
        }

        public void ClearActions()
        {
            if (actions.Count > 0) actions.Clear();
        }

        public void AdvanceAction()
        {
            if (actions.Count > 0)
                actions.RemoveAt(0);

            if (actions.Count <= 0) StopChain();
            else
            {
                if (party.Stamina.Empty || !CurrentController.Movement.Grounded)
                    StopChain();
                else TopAction.Execute();
            }
        }

        public void StartChain()
        {
            Executing = true;

            CurrentController.sm.ChangeState(StateID.ControllerAction);

            TopAction.Execute();
        }

        public void StopChain()
        {
            Executing = false;

            actions.Clear();
        }
    }
}