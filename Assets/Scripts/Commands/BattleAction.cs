using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class BattleAction : ICommand
    {
        [field: SerializeField] public ActionData Action { get; private set; }
        [field: SerializeField] public int AnimStateHash { get; protected set; }
        [SerializeField] protected Controller controller;
        [SerializeField] protected PartyController party;
        [field: SerializeField] public Vector3 Dir { get; protected set; } = 
            new Vector3(0, 0, 0);

        public BattleAction(Controller controller, ActionData action, int animStateHash)
        {
            this.controller = controller;
            Action = action;
            this.AnimStateHash = animStateHash;

            party = controller.Party;
        }

        public BattleAction(Controller controller, Vector3 dir, ActionData action, int animStateHash)
        {
            this.controller = controller;
            this.Dir = dir;
            Action = action;
            this.AnimStateHash = animStateHash;

            party = controller.Party;
        }

        public virtual void Execute()
        {
            if (controller.TargetSphere.Active)
                controller.Movement.FaceTarget(controller.TargetSphere.CurrentTargetTransform);
            else
            {
                if (Dir != Vector3.zero)
                    controller.transform.rotation = Quaternion.LookRotation(Dir);
            }

            controller.Model.PlayAnimationFade(AnimStateHash, 0, true);
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