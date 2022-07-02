using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    public class PartyController : MonoBehaviour
    {
        public event Action OnSpawn;
        public event Action OnCharacterChanged;

        public event Action OnHealthChanged;
        public event Action OnStaminaChanged;

        public event Action OnActionUse;

        public event Action OnDamage;
        public event Action OnDeath;

        int partyCap = 4;
        int healthCap = 3999;
        int staminaCap = 255;

        [field: SerializeField]
        public List<Controller> Party { get; private set; } = 
            new List<Controller>();

        [field: SerializeField] public Controller CurrentController { get; private set; }
        [field: SerializeField] public Vector3 CurrentPosition { get; private set; }
        [field: SerializeField] public Vector3 CurrentForward { get; private set; }

        public Health Health { get; private set; }
        public Stamina Stamina { get; private set; }
        public InputController InputController { get; private set; }
        public ActionQueue ActionQueue { get; private set; }

        public CameraFocus CamFocus { get; private set; }
        public Target Target { get; private set; }
        public TargetSphere TargetSphere { get; private set; }

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

                    return Mathf.RoundToInt(sp / Party.Count);
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

            CamFocus = GetComponentInChildren<CameraFocus>();
            Target = GetComponentInChildren<Target>();
            TargetSphere = GetComponentInChildren<TargetSphere>();
        }

        private void OnEnable()
        {
            InputController.DpadAction += DpadInput;
        }

        private void OnDisable()
        {
            InputController.DpadAction -= DpadInput;
        }

        private void Start()
        {
            if (gameObject.tag == "Player") Init(true);
            else Init(false);
        }

        private void Update()
        {
            CurrentController.UpdateController();

            UpdatePosition();
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

            SetCharsActive(0);

            CurrentController.sm.ChangeState(StateID.ControllerMove);

            Health.Init(Hp, Hp, CurrentCombatant.HRegen, healthCap);
            Stamina.Init(Sp, Sp, CurrentCombatant.SRegen, staminaCap);
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
            if (Party[index] == CurrentController) return;

            if (index >= Party.Count) return;

            UpdatePosition();

            var state = CurrentController.CurrentState;

            SetCharsActive(index);

            CurrentController.sm.ChangeState(state);

            InvokeCharChange();
        }

        void SetCharsActive(int index)
        {
            foreach (var p in Party) p.gameObject.SetActive(false);

            Party[index].gameObject.SetActive(true);
            CurrentController = Party[index];
        }

        void DpadInput(Vector2 dirInput)
        {
            if (CurrentController.sm.InState(StateID.ControllerMove, StateID.ControllerRun, 
                StateID.ControllerStrafe))
            {
                if (dirInput == Vector2.up) SwitchController(0);
                else if (dirInput == Vector2.left) SwitchController(1);
                else if (dirInput == Vector2.right) SwitchController(2);
                else if (dirInput == Vector2.down) SwitchController(3);
            }
        }

        #region Delegates
        public void InvokeSpawn()
        {
            OnSpawn?.Invoke();
        }

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

        public void InvokeActionUse()
        {
            OnActionUse?.Invoke();
        }

        public void InvokeDamage()
        {
            OnDamage?.Invoke();
        }

        public void InvokeDeath()
        {
            OnDeath?.Invoke();
        }
        #endregion
    }
}