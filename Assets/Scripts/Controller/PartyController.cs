using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PartyController : MonoBehaviour
    {
        public Action OnCharacterChanged;

        public Action OnHealthChanged;
        public Action OnStaminaChanged;

        public Action OnDeath;

        int partyCap = 4;

        [SerializeField] List<Controller> party = new List<Controller>();
        [SerializeField] int currentIndex;

        [SerializeField] Vector3 currentPosition;
        [SerializeField] Vector3 currentForward;

        Health health;
        Stamina stamina;
        InputController inputController;
        ActionQueue actionQueue;

        CameraPivot pivot;

        public Controller CurrentController
        {
            get
            {
                if (party.Count > 0) return party[currentIndex];
                return null;
            }
        }

        public Health Health => health;
        public Stamina Stamina => stamina;
        public InputController InputController => inputController;
        public ActionQueue ActionQueue => actionQueue;

        public CameraPivot Pivot => pivot;

        public int Hp { get; }
        public int Sp { get; }

        private void Awake()
        {
            health = GetComponent<Health>();
            stamina = GetComponent<Stamina>();
            inputController = GetComponent<InputController>();
            actionQueue = GetComponent<ActionQueue>();

            pivot = GetComponentInChildren<CameraPivot>();
        }

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            if (InputController.Char1()) SwitchController(0);
            else if (InputController.Char2()) SwitchController(1);
            else
            {
                CurrentController.UpdateController();

                UpdatePosition();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(currentPosition, 1.5f);
        }

        public void Init()
        {
            var controllers = GetComponentsInChildren<Controller>();

            foreach (var c in controllers)
            {
                if (party.Count < partyCap)
                {
                    c.Init();
                    party.Add(c);
                }
            }

            SwitchController(0);

            pivot.Init();

            health.Init(Hp, Hp, 1, 3999);
            stamina.Init(Sp, Sp, 10, 999);
        }

        void UpdatePosition()
        {
            if (CurrentController != null)
            {
                currentPosition = CurrentController.transform.position;
                currentForward = CurrentController.Model.transform.forward;
            }
        }

        public void SwitchController(int index)
        {
            if (index == currentIndex) return;

            currentIndex = Mathf.Clamp(index, 0, party.Count - 1);

            for(int i = 0; i < party.Count; i++)
            {
                if (i == currentIndex)
                {
                    party[i].gameObject.SetActive(true);
                    CurrentController.transform.position = currentPosition;
                    CurrentController.Model.transform.forward = currentForward;
                }
                else party[i].gameObject.SetActive(false);
            }
        }

        #region Delegates
        #endregion
    }
}