using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class Cooldown
    {
        [SerializeField] float cooldown = 1f;
        [SerializeField] float speed = 1f;
        [SerializeField] float count = 0f;

        public float CooldownValue
        {
            get => cooldown;
            set => cooldown = Mathf.Abs(value);
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public float Count
        {
            get => count;
            set => count = Mathf.Clamp(value, 0, cooldown);
        }

        public float CooldownFraction
        {
            get => count / cooldown;
            set => count = Mathf.Clamp(value * cooldown, 0, cooldown);
        }

        public bool Empty => count == 0;
        public bool Full => count == cooldown;

        public Cooldown(float cooldown)
        {
            this.cooldown = cooldown;
            speed = 1;
            count = 0;
        }

        public Cooldown(float cooldown, float speed)
        {
            this.cooldown = cooldown;
            this.speed = speed;
            count = 0;
        }

        public Cooldown(float cooldown, float speed, float init_count)
        {
            this.cooldown = cooldown;
            this.speed = speed;
            count = init_count;
        }

        public void Tick()
        {
            count = Mathf.Clamp(count + speed * Time.deltaTime, 0, cooldown);
        }

        public void Tick(float dt)
        {
            count = Mathf.Clamp(count + speed * dt, 0, cooldown);
        }

        public void Reset()
        {
            count = 0;
        }

        public void Fill()
        {
            count = cooldown;
        }
    }
}