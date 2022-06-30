using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class CharInfo : MonoBehaviour, IBattleUIElement
    {
        [SerializeField] PartyController party;

        [SerializeField] ResourceBar healthBar;
        [SerializeField] ResourceBar staminaBar;

        [SerializeField] Text lvText;
        [SerializeField] Text charNameText;

        public void InitUI(PartyController party)
        {
            this.party = party;

            healthBar.InitUI(party);
            staminaBar.InitUI(party);

            charNameText.text = party.CurrentCombatant.CharName;

            SubscribeToDelegates();
        }

        public void SubscribeToDelegates()
        {
            healthBar.SubscribeToDelegates();
            staminaBar.SubscribeToDelegates();

            party.OnCharacterChanged += UpdateUI;
        }

        public void UnsubscribeFromDelegates()
        {
            healthBar.UnsubscribeFromDelegates();
            staminaBar.UnsubscribeFromDelegates();

            party.OnCharacterChanged -= UpdateUI;
        }

        void UpdateUI()
        {
            charNameText.text = party.CurrentCombatant.CharName;
        }
    }
}