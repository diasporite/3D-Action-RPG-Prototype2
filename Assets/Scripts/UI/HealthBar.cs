using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class HealthBar : ResourceBar
    {
        public override void InitUI(PartyController party)
        {
            resource = party.Health;
            textHeader = "HP ";

            base.InitUI(party);
        }

        public override void SubscribeToDelegates()
        {
            party.OnHealthChanged += UpdateUI;
        }

        public override void UnsubscribeFromDelegates()
        {
            party.OnHealthChanged -= UpdateUI;
        }

        public override void UpdateUI()
        {
            base.UpdateUI();

            if (resource.ResourceFraction < 0.2f) fill.color = new Color(0.7843f, 0, 0);
            else fill.color = new Color(0, 0.7843f, 0);
        }
    }
}