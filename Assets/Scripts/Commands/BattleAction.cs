using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class BattleAction : ICommand
    {
        [SerializeField] protected string actionName;
        [SerializeField] protected string animationStateName;

        [SerializeField] Controller controller;

        public BattleAction(Controller controller)
        {
            this.controller = controller;
        }

        public virtual void Execute()
        {

        }

        public virtual IEnumerator ExecuteCo()
        {
            yield return null;
        }
    }
}