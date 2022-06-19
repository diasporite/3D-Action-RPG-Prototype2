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
        [SerializeField] Vector3 dir = new Vector3(0, 0, 0);

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

        public BattleAction(Controller controller, Vector3 dir, string actionName, int animStateHash, int spCost)
        {
            this.controller = controller;
            this.dir = dir;
            this.actionName = actionName;
            this.animStateHash = animStateHash;
            this.spCost = spCost;
            party = controller.Party;
        }

        public virtual void Execute()
        {
            if (dir != Vector3.zero) controller.transform.rotation = Quaternion.LookRotation(dir);
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