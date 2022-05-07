using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class BattleAction : ICommand
    {
        [SerializeField] protected string actionName;
        [SerializeField] protected string animationStateName;

        [SerializeField] Controller controller;

        public string AnimationStateName => animationStateName;

        public BattleAction(Controller controller)
        {
            this.controller = controller;
        }

        public BattleAction(Controller controller, string actionName)
        {
            this.controller = controller;
            this.actionName = actionName;
        }

        public BattleAction(Controller controller, string actionName, string animationStateName)
        {
            this.controller = controller;
            this.actionName = actionName;
            this.animationStateName = animationStateName;
        }

        public virtual void Execute()
        {

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