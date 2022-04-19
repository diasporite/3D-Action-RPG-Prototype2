using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Stamina : Resource
    {
        public override ResourceState State
        {
            get => base.State;
            set
            {
                value = base.State;

                switch (value)
                {
                    case ResourceState.Regen:
                        currentRegen = regen;
                        break;
                    case ResourceState.Run:
                        currentRegen = -0.2f * regen;
                        break;
                    case ResourceState.Recover:
                        currentRegen = 2.5f * regen;
                        break;
                }

                resourceCooldown.Speed = currentRegen;
            }
        }
    }
}