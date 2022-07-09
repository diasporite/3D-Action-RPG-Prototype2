using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class StaminaBar : ResourceBar
    {
        Stamina stamina;

        public override void InitUI(PartyController party)
        {
            resource = party.Stamina;
            stamina = party.Stamina;
            textHeader = "SP ";

            base.InitUI(party);
        }

        public override void SubscribeToDelegates()
        {
            party.OnStaminaChanged += UpdateUI;
        }

        public override void UnsubscribeFromDelegates()
        {
            party.OnStaminaChanged -= UpdateUI;
        }

        public override void UpdateUI()
        {
            base.UpdateUI();

            if (stamina.InRecovery) fill.color = Color.grey;
            else fill.color = new Color(0, 0, 0.7843f);
        }
    }
}