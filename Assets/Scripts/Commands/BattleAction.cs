using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class BattleAction : ICommand
    {
        [SerializeField] protected string actionName;
        [SerializeField] protected int animStateHash;
        [SerializeField] int spCost;
        [SerializeField] Controller controller;
        [SerializeField] PartyController party;

        public int AnimStateHash => animStateHash;

        public BattleAction(Controller controller)
        {
            this.controller = controller;
            party = controller.Party;
        }

        public BattleAction(Controller controller, string actionName)
        {
            this.controller = controller;
            this.actionName = actionName;
            party = controller.Party;
        }

        public BattleAction(Controller controller, string actionName, int animStateHash, int spCost)
        {
            this.controller = controller;
            this.actionName = actionName;
            this.animStateHash = animStateHash;
            this.spCost = spCost;
            party = controller.Party;
        }

        public virtual void Execute()
        {
            controller.Model.PlayAnimation(animStateHash, 0);
            controller.Party.Stamina.ChangeValue(-Mathf.Abs(spCost));
        }

        public virtual void Undo()
        {

        }

        public virtual IEnumerator ExecuteCo()
        {
            yield return null;
        }

        public virtual IEnumerator UndoCo()
        {
            yield return null;
        }
    }
}