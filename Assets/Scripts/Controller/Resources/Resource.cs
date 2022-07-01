using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Resource : MonoBehaviour
    {
        protected PartyController party;

        [SerializeField] protected PointStat resourcePoints = new PointStat(100);
        [SerializeField] protected Cooldown resourceCooldown = new Cooldown(100);

        [SerializeField] protected float regen;
        [SerializeField] protected float currentRegen;

        public bool Empty => resourcePoints.Empty;
        public bool Full => resourcePoints.Full;

        public int ResourcePointValue => resourcePoints.PointValue;
        public int ResourceStatValue => resourcePoints.CurrentStatValue;

        public float ResourceFraction => resourceCooldown.CooldownFraction;

        private void Awake()
        {
            party = GetComponent<PartyController>();
        }

        public virtual void Init(int initValue, int initPoints, int regen, int statCap)
        {
            this.regen = regen;
            currentRegen = regen;

            resourcePoints = new PointStat(initValue, initPoints, statCap);
            resourceCooldown = new Cooldown(initValue, regen, initPoints);
        }

        public virtual void Tick()
        {
            resourceCooldown.Tick();
            resourcePoints.PointValue = Mathf.CeilToInt(resourceCooldown.Count);
        }

        public virtual void Tick(float dt)
        {
            resourceCooldown.Tick(dt);
            resourcePoints.PointValue = Mathf.CeilToInt(resourceCooldown.Count);
        }

        public virtual void ChangeValue(int amount)
        {
            resourcePoints.PointValue += amount;
            resourceCooldown.Count += amount;
        }
    }
}