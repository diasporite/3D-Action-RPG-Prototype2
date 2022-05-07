using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Health : Resource
    {
        public override ResourceState State
        {
            get => base.State;
            set
            {
                switch (value)
                {
                    case ResourceState.Regen:
                        currentRegen = regen;
                        break;
                    case ResourceState.Run:
                        currentRegen = 0;
                        break;
                    case ResourceState.Recover:
                        currentRegen = 0;
                        break;
                }

                resourceCooldown.Speed = currentRegen;

                state = value;
            }
        }

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