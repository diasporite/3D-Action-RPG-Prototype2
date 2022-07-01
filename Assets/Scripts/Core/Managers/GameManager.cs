using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        public UIManager ui;

        public CombatDatabase combat;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);

            ui = FindObjectOfType<UIManager>();

            combat.InitDatabase();
        }

        private void Start()
        {
            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            var spawner = FindObjectOfType<PlayerSpawner>();
            spawner.Spawn();
            ui.InitUI(spawner.GetComponent<PartyController>());
        }

        public void InitUI(PartyController player)
        {
            ui.InitUI(player);
        }
    }
}