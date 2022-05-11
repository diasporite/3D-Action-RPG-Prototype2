using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] BattleHUD battle;

        public void InitUI(PartyController party)
        {
            battle.InitUI(party);
        }
    }
}