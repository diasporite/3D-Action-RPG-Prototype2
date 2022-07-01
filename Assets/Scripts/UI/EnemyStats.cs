using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] float height = 80f;
        [SerializeField] float lifetime = 3f;

        Camera mainCam;

        [SerializeField] PartyController party;

        [SerializeField] HealthBar health;
        [SerializeField] StaminaBar stamina;

        [SerializeField] DamageText damageText;

        [field: SerializeField] public Cooldown Timer { get; private set; }

        private void Awake()
        {
            mainCam = Camera.main;

            party = GetComponentInParent<PartyController>();

            health = GetComponentInChildren<HealthBar>();
            stamina = GetComponentInChildren<StaminaBar>();

            //damageText = GetComponentInChildren<DamageText>();

            Timer = new Cooldown(lifetime, 1, lifetime);
        }

        private void Start()
        {
            health.InitUI(party);
            stamina.InitUI(party);

            health.gameObject.SetActive(false);
            stamina.gameObject.SetActive(false);

            //damageText.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            party.OnDamage += ShowStats;
        }

        private void OnDisable()
        {
            party.OnDamage -= ShowStats;
        }

        private void Update()
        {
            if (!Timer.Full)
            {
                Timer.Tick();

                UpdatePosition();

                if (Timer.Full)
                {
                    health.gameObject.SetActive(false);
                    stamina.gameObject.SetActive(false);

                    //damageText.gameObject.SetActive(false);
                }
            }
        }

        void ShowStats()
        {
            UpdatePosition();

            Timer.Reset();

            health.gameObject.SetActive(true);
            stamina.gameObject.SetActive(true);

            //damageText.gameObject.SetActive(true);
        }

        void UpdatePosition()
        {
            transform.position = mainCam.WorldToScreenPoint(party.transform.position) + 
                height * Vector3.up;
        }
    }
}