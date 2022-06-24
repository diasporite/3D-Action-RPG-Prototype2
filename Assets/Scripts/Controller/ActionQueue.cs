using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class ActionQueue : MonoBehaviour
    {
        int actionCap = 5;

        [field: SerializeField] public bool Executing { get; private set; }

        [field: SerializeField] public List<BattleAction> Actions { get; private set; } = 
            new List<BattleAction>();

        PartyController party;

        public BattleAction TopAction
        {
            get
            {
                if (Actions.Count > 0) return Actions[0];
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
            if (Actions.Count < actionCap)
                Actions.Add(action);

            if (!Executing) StartChain();
        }

        public void ClearActions()
        {
            if (Actions.Count > 0) Actions.Clear();
        }

        public void AdvanceAction()
        {
            if (Actions.Count > 0)
                Actions.RemoveAt(0);

            if (Actions.Count <= 0) StopChain();
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

            Actions.Clear();
        }
    }
}