using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class PointStatf : Statf
    {
        [SerializeField] float pointValue;

        public float PointValue
        {
            get => pointValue;
            set => pointValue = Mathf.Clamp(value, 0, CurrentStatValue);
        }

        public float PointFraction
        {
            get => (float)pointValue / CurrentStatValue;
            set => pointValue = Mathf.RoundToInt(CurrentStatValue * Mathf.Clamp(value, 0, 1));
        }

        public bool Empty => pointValue <= 0;
        public bool Full => pointValue >= CurrentStatValue;

        public PointStatf(int statValue) : base(statValue)
        {
            pointValue = CurrentStatValue;
        }

        public PointStatf(int statValue, int statCap) : base(statValue, statCap)
        {
            pointValue = CurrentStatValue;
        }

        public PointStatf(int statValue, int pointValue, int statCap) : base(statValue, statCap)
        {
            this.pointValue = Mathf.Clamp(pointValue, 0, CurrentStatValue);
        }
    }
}