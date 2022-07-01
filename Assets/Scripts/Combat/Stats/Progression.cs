using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class Progression
    {
        [field: SerializeField] public int Level { get; private set; }

        int levelCap = 1;

        public Progression(int level, int levelCap)
        {
            Level = level;
            this.levelCap = levelCap;
        }

        public void ChangeLevel(int amount)
        {
            Level = Mathf.Clamp(Level + amount, 1, levelCap);
        }
    }
}