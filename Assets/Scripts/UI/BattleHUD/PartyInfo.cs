using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class PartyInfo : MonoBehaviour, IBattleUIElement
    {
        [SerializeField] PartyController party;

        [SerializeField] PartyPanel[] characters;

        private void Awake()
        {
            characters = GetComponentsInChildren<PartyPanel>();
        }

        public void InitUI(PartyController party)
        {
            this.party = party;

            UpdateUI();

            SubscribeToDelegates();
        }

        public void SubscribeToDelegates()
        {

        }

        public void UnsubscribeFromDelegates()
        {

        }

        public void UpdateUI()
        {
            for (int i = 0; i < characters.Length; i++)
            {
                if (i < party.Party.Count)
                    characters[i].Character = party.Party[i].Combatant.Data;
                else characters[i].Character = null;

                characters[i].UpdateUI();
            }
        }
    }
}