using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class BattleHUD : MonoBehaviour
    {
        [SerializeField] PartyController player;

        [SerializeField] CharInfo charInfo;
        [SerializeField] SkillsetUI skillsetUI;
        [SerializeField] PartyInfo partyInfo;

        private void Awake()
        {
            charInfo = GetComponentInChildren<CharInfo>();
            skillsetUI = GetComponentInChildren<SkillsetUI>();
            partyInfo = GetComponentInChildren<PartyInfo>();
        }

        private void OnEnable()
        {
            if (player != null) SubscribeToDelegates();
        }

        private void OnDisable()
        {
            UnsubscribeFromDelegates();
        }

        public void InitUI(PartyController player)
        {
            this.player = player;

            charInfo.InitUI(player);
            skillsetUI.InitUI(player);
            partyInfo.InitUI(player);
        }

        private void SubscribeToDelegates()
        {
            charInfo.SubscribeToDelegates();
            skillsetUI.SubscribeToDelegates();
            partyInfo.SubscribeToDelegates();
        }

        private void UnsubscribeFromDelegates()
        {
            charInfo.UnsubscribeFromDelegates();
            skillsetUI.UnsubscribeFromDelegates();
            partyInfo.UnsubscribeFromDelegates();
        }
    }
}