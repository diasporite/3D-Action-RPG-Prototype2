using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [System.Serializable]
    public class Knockback
    {
        [SerializeField] float force = 0f;
        [SerializeField] float dt = 0f;
        [SerializeField] Vector3 dir = new Vector3(0, 0, 0);

        public Knockback(KnockbackData knockback, Vector3 forward)
        {
            force = knockback.KnockbackForce;
            dt = knockback.DragTime;

            dir = knockback.KnockbackDir.x * forward;
            dir.y = knockback.KnockbackDir.y;
        }

        public void Apply(Movement movement)
        {
            Debug.Log("knock");
            if (movement == null) return;
            if (force == 0f || dt == 0f) return;

            movement.ApplyForce(force * dir, dt);
        }
    }
}