using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG_Project
{
    public class Combatant : MonoBehaviour, IDamageable
    {
        CombatDatabase combat;

        [SerializeField] CharData data;

        [SerializeField] string charName;

        [field: Header("Progression")]
        [field: SerializeField] public Stat Level { get; private set; }

        #region Stats
        [field: SerializeField] public Stat Vitality { get; private set; } = new Stat(100, 255);
        [field: SerializeField] public Stat Endurance { get; private set; } = new Stat(100, 255);

        [field: SerializeField] public Stat Attack { get; private set; } = new Stat(100, 255);
        [field: SerializeField] public Stat Defence { get; private set; } = new Stat(100, 255);

        [field: SerializeField] public Stat Weight { get; private set; } = new Stat(128, 255);

        [field: SerializeField] public Stat HealthRegen { get; private set; } = new Stat(4, 15);
        [field: SerializeField] public Stat StaminaRegen { get; private set; } = new Stat(32, 63);
        #endregion

        #region StatAtLv
        [Header("Stats at Level")]
        [SerializeField] int[] vitAtLv;
        [SerializeField] int[] endAtLv;

        [SerializeField] int[] atkAtLv;
        [SerializeField] int[] defAtLv;

        [SerializeField] int[] hRegenAtLv;
        [SerializeField] int[] sRegenAtLv;
        #endregion

        // Only first 4 skills will be usable in combat
        [field: SerializeField] public List<ActionData> Skillset { get; private set; } = new List<ActionData>();

        PartyController party;
        Controller controller;
        Health health;
        Stamina stamina;

        public int Vit => Vitality.CurrentStatValue;
        public int End => Endurance.CurrentStatValue;

        public int Atk => Attack.CurrentStatValue;
        public int Def => Defence.CurrentStatValue;

        public int Wt => Weight.CurrentStatValue;

        public int HRegen => HealthRegen.CurrentStatValue;
        public int SRegen => StaminaRegen.CurrentStatValue;

        public ActionData[] CurrentSkillset
        {
            get
            {
                var skills = new ActionData[4];

                for (int i = 0; i < skills.Length; i++)
                {
                    if (i < Skillset.Count)
                        skills[i] = Skillset[i];
                    else skills[i] = null;
                }
                return skills;
            }
        }

        public ActionData GetActionData(int index)
        {
            index = Mathf.Clamp(index, 0, 4);

            if (index == 0) return data.DefendAction;

            return data.Actions[index - 1];
        }

        private void Awake()
        {
            party = GetComponentInParent<PartyController>();
            controller = GetComponent<Controller>();
        }

        public void OnDamage(DamageInfo damage)
        {
            int hDamage = combat.HealthDamage(damage, this);
            int sDamage = combat.StaminaDamage(damage, this);

            health.ChangeValue(-hDamage);
            stamina.ChangeValue(-sDamage);

            if (health.Empty)
                controller.sm.ChangeState(StateID.ControllerDeath);
            else if (stamina.Empty)
                controller.sm.ChangeState(StateID.ControllerStagger);
        }

        public void Init(int lv)
        {
            combat = GameManager.instance.combat;

            Level = new Stat(lv, 99);

            health = party.Health;
            stamina = party.Stamina;

            charName = data.CharName;

            //expAtLv = combat.GetStatAtLv(StatID.ExpAtLv, data.baseExpAtLv);
            
            vitAtLv = combat.GetStatAtLv(StatID.Vitality, data.BaseVit);
            endAtLv = combat.GetStatAtLv(StatID.Endurance, data.BaseEnd);

            atkAtLv = combat.GetStatAtLv(StatID.Attack, data.BaseAtk);
            defAtLv = combat.GetStatAtLv(StatID.Defence, data.BaseDef);

            hRegenAtLv = combat.GetStatAtLv(StatID.HealthRegen, data.BaseHealthRegen);
            sRegenAtLv = combat.GetStatAtLv(StatID.StaminaRegen, data.BaseStaminaRegen);

            //this.exp = exp;

            //level = CalculateLv();

            Vitality = new Stat(vitAtLv[Level.StatValue - 1], 255);
            Endurance = new Stat(endAtLv[Level.StatValue - 1], 255);

            Attack = new Stat(atkAtLv[Level.StatValue - 1], 255);
            Defence = new Stat(defAtLv[Level.StatValue - 1], 255);

            Weight = new Stat(data.Weight, 255);

            HealthRegen = new Stat(hRegenAtLv[Level.StatValue - 1], 15);
            StaminaRegen = new Stat(sRegenAtLv[Level.StatValue - 1], 63);

            Skillset = data.Actions.ToList();
        }

        //int CalculateLv()
        //{
        //    for (int i = 0; i < expAtLv.Length; i++)
        //        if (exp < expAtLv[i])
        //            return i;

        //    return expAtLv.Length;
        //}

        public void SwapSkills(ActionData skill1, ActionData skill2)
        {
            if (!Skillset.Contains(skill1) || !Skillset.Contains(skill2)) return;

            var i1 = Skillset.IndexOf(skill1);
            var i2 = Skillset.IndexOf(skill2);

            Skillset.RemoveAt(i1);
            Skillset.Insert(i1, skill2);

            Skillset.RemoveAt(i2);
            Skillset.Insert(i2, skill1);
        }
    }
}