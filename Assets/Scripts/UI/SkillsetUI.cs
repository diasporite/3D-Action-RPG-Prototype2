using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class SkillsetUI : MonoBehaviour
    {
        [SerializeField] PartyController party;

        SkillButton[] skills;

        private void Awake()
        {
            skills = GetComponentsInChildren<SkillButton>();
        }

        public void InitUI(PartyController party)
        {
            this.party = party;

            PopulateSkills();

            SubscribeToDelegates();
        }

        public void SubscribeToDelegates()
        {
            party.OnCharacterChanged += PopulateSkills;
        }

        public void UnsubscribeFromDelegates()
        {
            party.OnCharacterChanged -= PopulateSkills;
        }

        void PopulateSkills()
        {
            var combatant = party.CurrentCombatant;

            for (int i = 0; i < skills.Length; i++)
            {
                if (i < combatant.Skillset.Count)
                    skills[i].Skill = combatant.Skillset[i];
                else skills[i].Skill = null;

                skills[i].UpdateUI();
            }
        }
    }
}