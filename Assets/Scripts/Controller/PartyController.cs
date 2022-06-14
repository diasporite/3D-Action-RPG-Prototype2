using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PartyController : MonoBehaviour
    {
        public event Action OnCharacterChanged;

        public event Action OnHealthChanged;
        public event Action OnStaminaChanged;

        public event Action OnDeath;

        int partyCap = 4;
        int healthCap = 3999;
        int staminaCap = 999;

        [SerializeField] List<Controller> party = new List<Controller>();
        [SerializeField] int currentIndex;

        [SerializeField] Vector3 currentPosition;
        [SerializeField] Vector3 currentForward;

        Health health;
        Stamina stamina;
        InputController inputController;
        ActionQueue actionQueue;

        CameraPivot pivot;
        TargetSphere targetSphere;

        public Controller CurrentController
        {
            get
            {
                if (party.Count > 0) return party[currentIndex];
                return null;
            }
        }
        public Transform CurrentControllerTransform => CurrentController?.transform;
        public Combatant CurrentCombatant => CurrentController?.Combatant;

        public Health Health => health;
        public Stamina Stamina => stamina;
        public InputController InputController => inputController;
        public ActionQueue ActionQueue => actionQueue;

        public CameraPivot Pivot => pivot;
        public TargetSphere TargetSphere => targetSphere;

        public int Hp
        {
            get
            {
                var hp = 0;

                if (party.Count > 0)
                {
                    foreach (var c in party)
                        hp += c.Combatant.Vit;

                    hp = Mathf.RoundToInt(hp / party.Count);

                    return Mathf.RoundToInt(hp * (1 + 0.75f * (party.Count - 1)));
                }

                return 1;
            }
        }

        public int Sp
        {
            get
            {
                var sp = 0;

                if (party.Count > 0)
                {
                    foreach (var c in party)
                        sp += c.Combatant.End;

                    sp = Mathf.RoundToInt(sp / party.Count);

                    return Mathf.RoundToInt(sp * (1 + 0.15f * (party.Count - 1)));
                }

                return 1;
            }
        }

        private void Awake()
        {
            health = GetComponent<Health>();
            stamina = GetComponent<Stamina>();
            inputController = GetComponent<InputController>();
            actionQueue = GetComponent<ActionQueue>();

            pivot = GetComponentInChildren<CameraPivot>();
            targetSphere = GetComponentInChildren<TargetSphere>();
        }

        private void Start()
        {
            Init(true);
        }

        private void Update()
        {
            //if (InputController.Char1()) SwitchController(0);
            //else if (InputController.Char2()) SwitchController(1);
            //else
            //{
            CurrentController.UpdateController();

            UpdatePosition();
            //}
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(currentPosition, 1.5f);
        }

        public void Init(bool isPlayerParty)
        {
            var controllers = GetComponentsInChildren<Controller>();

            foreach (var c in controllers)
            {
                if (party.Count < partyCap)
                {
                    c.Init(isPlayerParty);
                    party.Add(c);
                }
            }

            SwitchController(0);

            pivot.Init();

            health.Init(Hp, Hp, CurrentCombatant.HRegen, healthCap);
            stamina.Init(Sp, Sp, CurrentCombatant.SRegen, staminaCap);

            CurrentController.sm.ChangeState(StateID.ControllerMove);
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

            InvokeCharChange();
        }

        #region Delegates
        public void InvokeCharChange()
        {
            OnCharacterChanged?.Invoke();
        }

        public void InvokeHpChange()
        {
            OnHealthChanged?.Invoke();
        }

        public void InvokeSpChange()
        {
            OnStaminaChanged?.Invoke();
        }

        public void InvokeDeath()
        {
            OnDeath?.Invoke();
        }
        #endregion
    }
}