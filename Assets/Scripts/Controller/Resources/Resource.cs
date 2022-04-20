using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public enum ResourceState
    {
        None = 0,
        Regen = 1,
        Run = 2,
        Recover = 3,
    }

    public class Resource : MonoBehaviour
    {
        [SerializeField] protected PointStat resourcePoints = new PointStat(100);
        [SerializeField] protected Cooldown resourceCooldown = new Cooldown(100);

        [SerializeField] protected ResourceState state;
        [SerializeField] protected float regen;
        [SerializeField] protected float currentRegen;

        public virtual ResourceState State
        {
            get => state;
            set => state = value;
        }

        public bool Empty => resourcePoints.Empty;
        public bool Full => resourcePoints.Full;

        public float ResourceFraction => resourceCooldown.CooldownFraction;

        public virtual void Init(int initValue, int initPoints, int regen, int statCap)
        {
            this.regen = regen;
            currentRegen = regen;

            resourcePoints = new PointStat(initValue, initPoints, statCap);
            resourceCooldown = new Cooldown(initValue, regen, initPoints);
        }

        public void Tick()
        {
            resourceCooldown.Tick();
            resourcePoints.PointValue = Mathf.CeilToInt(resourceCooldown.Count);
        }

        public void Tick(float dt)
        {
            resourceCooldown.Tick(dt);
            resourcePoints.PointValue = Mathf.CeilToInt(resourceCooldown.Count);
        }

        public void ChangeValue(int amount)
        {
            resourcePoints.PointValue += amount;
            resourceCooldown.Count += amount;
        }
    }
}