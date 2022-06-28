using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Stamina : Resource
    {
        public override void Tick()
        {
            base.Tick();

            party.InvokeSpChange();
        }

        public override void Tick(float dt)
        {
            base.Tick(dt);

            party.InvokeSpChange();
        }

        public override void ChangeValue(int amount)
        {
            base.ChangeValue(amount);

            party.InvokeSpChange();
        }
    }
}