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

        [field: SerializeField]
        public List<Controller> Party { get; private set; } = 
            new List<Controller>();
        [field: SerializeField] public int CurrentIndex { get; private set; }

        [field: SerializeField] public Vector3 CurrentPosition { get; private set; }
        [field: SerializeField] public Vector3 CurrentForward { get; private set; }

        public Health Health { get; private set; }
        public Stamina Stamina { get; private set; }
        public InputController InputController { get; private set; }
        public ActionQueue ActionQueue { get; private set; }

        public TargetSphere TargetSphere { get; private set; }

        public Controller CurrentController
        {
            get
            {
                if (Party.Count > 0) return Party[CurrentIndex];
                return null;
            }
        }
        public Transform CurrentControllerTransform => CurrentController?.transform;
        public Combatant CurrentCombatant => CurrentController?.Combatant;

        public int Hp
        {
            get
            {
                var hp = 0;

                if (Party.Count > 0)
                {
                    foreach (var c in Party)
                        hp += c.Combatant.Vit;

                    hp = Mathf.RoundToInt(hp / Party.Count);

                    return Mathf.RoundToInt(hp * (1 + 0.75f * (Party.Count - 1)));
                }

                return 1;
            }
        }

        public int Sp
        {
            get
            {
                var sp = 0;

                if (Party.Count > 0)
                {
                    foreach (var c in Party)
                        sp += c.Combatant.End;

                    sp = Mathf.RoundToInt(sp / Party.Count);

                    return Mathf.RoundToInt(sp * (1 + 0.15f * (Party.Count - 1)));
                }

                return 1;
            }
        }

        private void Awake()
        {
            Health = GetComponent<Health>();
            Stamina = GetComponent<Stamina>();
            InputController = GetComponent<InputController>();
            ActionQueue = GetComponent<ActionQueue>();

            TargetSphere = GetComponentInChildren<TargetSphere>();
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
            Gizmos.DrawWireSphere(CurrentPosition, 1.5f);
        }

        public void Init(bool isPlayerParty)
        {
            var controllers = GetComponentsInChildren<Controller>();

            foreach (var c in controllers)
            {
                if (Party.Count < partyCap)
                {
                    c.Init(isPlayerParty);
                    Party.Add(c);
                }
            }

            SwitchController(0);

            Health.Init(Hp, Hp, CurrentCombatant.HRegen, healthCap);
            Stamina.Init(Sp, Sp, CurrentCombatant.SRegen, staminaCap);

            CurrentController.sm.ChangeState(StateID.ControllerMove);
        }

        void UpdatePosition()
        {
            if (CurrentController != null)
            {
                CurrentPosition = CurrentController.transform.position;
                CurrentForward = CurrentController.Model.transform.forward;
            }
        }

        public void SwitchController(int index)
        {
            if (index == CurrentIndex) return;

            CurrentIndex = Mathf.Clamp(index, 0, Party.Count - 1);

            for(int i = 0; i < Party.Count; i++)
            {
                if (i == CurrentIndex)
                {
                    Party[i].gameObject.SetActive(true);
                    CurrentController.transform.position = CurrentPosition;
                    CurrentController.Model.transform.forward = CurrentForward;
                }
                else Party[i].gameObject.SetActive(false);
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