using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class Hitbox : MonoBehaviour
    {
        public readonly List<IDamageable> hits = new List<IDamageable>();

        public Combatant Instigator { get; private set; }

        private void Awake()
        {
            Instigator = GetComponentInParent<Combatant>();
        }

        private void OnEnable()
        {
            hits.Clear();
        }

        private void OnDisable()
        {
            hits.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other != null)
            {
                var hurt = other.GetComponent<Hurtbox>();

                if (hurt != null)
                {
                    if (hurt.transform.root == transform.root) return;

                    var damageable = hurt.Damageable;

                    if (damageable == null) return;

                    if (!hits.Contains(damageable))
                    {
                        print(3);
                        //hits.Add(damageable);
                        damageable.OnDamage(Instigator.CurrentAction.Info(Instigator), 
                            Instigator.CurrentAction.Knockback(Instigator.transform.forward));
                    }
                }
            }
        }

        DamageInfo GetDamage(Hurtbox hurt)
        {
            if (hurt.HurtboxState == HurtboxState.Acting)
                return new DamageInfo(Instigator, 75, 40);
            else if (hurt.HurtboxState == HurtboxState.Guard)
                return new DamageInfo(Instigator, 5, 40);
            else if (hurt.HurtboxState == HurtboxState.Roll)
                return new DamageInfo(Instigator, 15, 0);

            return new DamageInfo(Instigator, 40, 15);
        }
    }
}