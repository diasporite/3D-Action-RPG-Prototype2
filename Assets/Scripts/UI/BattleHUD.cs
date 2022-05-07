using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class BattleHUD : MonoBehaviour
    {
        [SerializeField] PartyController player;

        public void InitUI(PartyController player)
        {
            this.player = player;
        }
    }
}