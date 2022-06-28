using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Health : Resource
    {
        public override void Tick()
        {
            base.Tick();

            party.InvokeHpChange();
        }

        public override void Tick(float dt)
        {
            base.Tick(dt);

            party.InvokeHpChange();
        }

        public override void ChangeValue(int amount)
        {
            base.ChangeValue(amount);

            party.InvokeHpChange();
        }
    }
}