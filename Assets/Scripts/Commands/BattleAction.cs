using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class BattleAction : ICommand
    {
        [field: SerializeField] public ActionData Action { get; private set; }
        [SerializeField] protected int animStateHash;
        [SerializeField] Controller controller;
        [SerializeField] PartyController party;
        [SerializeField] Vector3 dir = new Vector3(0, 0, 0);

        public int AnimStateHash => animStateHash;

        public BattleAction(Controller controller, ActionData action, int animStateHash)
        {
            this.controller = controller;
            Action = action;
            this.animStateHash = animStateHash;

            party = controller.Party;
        }

        public BattleAction(Controller controller, Vector3 dir, ActionData action, int animStateHash)
        {
            this.controller = controller;
            this.dir = dir;
            Action = action;
            this.animStateHash = animStateHash;

            party = controller.Party;
        }

        public virtual void Execute()
        {
            if (dir != Vector3.zero) controller.transform.rotation = Quaternion.LookRotation(dir);
            controller.Model.PlayAnimationFade(animStateHash, 0, true);
            controller.Party.Stamina.ChangeValue(-Mathf.Abs(Action.SpCost));
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