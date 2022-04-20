using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class PointStat : Stat
    {
        [SerializeField] int pointValue;

        public int PointValue
        {
            get => PointValue;
            set => pointValue = Mathf.Clamp(value, 0, currentStatValue);
        }

        public float PointFraction
        {
            get => (float)pointValue / currentStatValue;
            set => pointValue = Mathf.RoundToInt(currentStatValue * Mathf.Clamp(value, 0, 1));
        }

        public bool Empty => pointValue <= 0;
        public bool Full => pointValue >= currentStatValue;

        public PointStat(int statValue) : base(statValue)
        {
            pointValue = currentStatValue;
        }

        public PointStat(int statValue, int statCap) : base(statValue, statCap)
        {
            pointValue = currentStatValue;
        }

        public PointStat(int statValue, int pointValue, int statCap) : base(statValue, statCap)
        {
            this.pointValue = Mathf.Clamp(pointValue, 0, currentStatValue);
        }
    }
}