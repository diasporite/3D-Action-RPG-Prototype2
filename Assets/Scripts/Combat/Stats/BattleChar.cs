using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class BattleChar
    {
        [SerializeField] string charName;

        [SerializeField] Stat vitality = new Stat(100, 255);
        [SerializeField] Stat endurance = new Stat(100, 255);

        [SerializeField] Stat attack = new Stat(100, 255);
        [SerializeField] Stat defence = new Stat(100, 255);

        [SerializeField] Stat weight = new Stat(128, 255);
    }
}