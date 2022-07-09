using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Stamina : Resource
    {
        [field: SerializeField] public bool InRecovery { get; private set; }

        public override void Tick()
        {
            base.Tick();

            party.InvokeSpChange();

            if (Empty && !InRecovery) InRecovery = true;
            if (Full && InRecovery) InRecovery = false;
        }

        public override void Tick(float dt)
        {
            base.Tick(dt);

            party.InvokeSpChange();

            if (Empty && !InRecovery) InRecovery = true;
            if (Full && InRecovery) InRecovery = false;
        }

        public override void ChangeValue(int amount)
        {
            base.ChangeValue(amount);

            party.InvokeSpChange();
        }
    }
}