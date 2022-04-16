using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] PointStat resourcePoints = new PointStat(100);
        [SerializeField] Cooldown resourceCooldown = new Cooldown(100);

        public bool Empty => resourcePoints.Empty;
        public bool Full => resourcePoints.Full;

        public virtual void Init(int initValue, int initPoints, int regen, int statCap)
        {
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