using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class CharInfo : MonoBehaviour, IBattleUIElement
    {
        [SerializeField] ResourceBar healthBar;
        [SerializeField] ResourceBar staminaBar;

        public void InitUI(PartyController party)
        {
            healthBar.InitUI(party);
            staminaBar.InitUI(party);

            SubscribeToDelegates();
        }

        public void SubscribeToDelegates()
        {
            healthBar.SubscribeToDelegates();
            staminaBar.SubscribeToDelegates();
        }

        public void UnsubscribeFromDelegates()
        {
            healthBar.UnsubscribeFromDelegates();
            staminaBar.UnsubscribeFromDelegates();
        }
    }
}