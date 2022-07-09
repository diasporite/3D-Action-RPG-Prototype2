using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class DashAction : BattleAction
    {
        public DashAction(Controller controller, Vector3 dir, ActionData action, 
            int animStateHash) : base(controller, dir, action, animStateHash)
        {

        }

        public override void Execute()
        {
            controller.Combatant.CurrentAction = Action;

            //if (controller.TargetSphere.Active)
            //    controller.Movement.FaceTarget(controller.TargetSphere.CurrentTargetTransform);
            //else
            //{
            //    if (Dir != Vector3.zero)
            //        controller.transform.rotation = Quaternion.LookRotation(Dir);
            //}

            controller.Model.PlayAnimationFade(AnimStateHash, 0, true);
            controller.Party.Stamina.ChangeValue(-Mathf.Abs(Action.SpCost));
        }
    }
}