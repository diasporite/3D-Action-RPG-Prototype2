using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class CharInfo : MonoBehaviour
    {
        [SerializeField] ResourceBar healthBar;
        [SerializeField] ResourceBar staminaBar;

        public void InitUI(PartyController party)
        {
            healthBar.InitUI(party);
            staminaBar.InitUI(party);
        }
    }
}