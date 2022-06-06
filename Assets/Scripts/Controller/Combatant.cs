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

        [Header("Progression")]
        [SerializeField] int level = 1;
        //[SerializeField] int exp = 0;
        //[SerializeField] int[] expAtLv;

        #region Stats
        [Header("Resource Stats")]
        [SerializeField] Stat vitality = new Stat(100, 255);
        [SerializeField] Stat endurance = new Stat(100, 255);

        [Header("Damage Stats")]
        [SerializeField] Stat attack = new Stat(100, 255);
        [SerializeField] Stat defence = new Stat(100, 255);

        [SerializeField] Stat weight = new Stat(128, 255);

        [Header("Regen Stats")]
        [SerializeField] Stat healthRegen = new Stat(4, 15);
        [SerializeField] Stat staminaRegen = new Stat(32, 63);
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
        [SerializeField] List<ActionData> skillset = new List<ActionData>();

        PartyController party;
        Controller controller;
        Health health;
        Stamina stamina;

        public Stat Vitality => vitality;
        public Stat Endurance => endurance;

        public Stat Attack => attack;
        public Stat Defence => defence;

        public Stat Weight => weight;

        public Stat HealthRegen => healthRegen;
        public Stat StaminaRegen => staminaRegen;

        public int Vit => vitality.CurrentStatValue;
        public int End => endurance.CurrentStatValue;

        public int Atk => attack.CurrentStatValue;
        public int Def => defence.CurrentStatValue;

        public int Wt => weight.CurrentStatValue;

        public int HRegen => healthRegen.CurrentStatValue;
        public int SRegen => staminaRegen.CurrentStatValue;

        public List<ActionData> Skillset => skillset;
        public ActionData[] CurrentSkillset
        {
            get
            {
                var skills = new ActionData[4];

                for(int i = 0; i < skills.Length; i++)
                {
                    if (i < skillset.Count)
                        skills[i] = skillset[i];
                    else skills[i] = null;
                }
                return skills;
            }
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

            if (health.Empty) controller.sm.ChangeState(StateID.ControllerDeath);
            else if (stamina.Empty) controller.sm.ChangeState(StateID.ControllerStagger);
        }

        public void Init(int exp)
        {
            combat = GameManager.instance.combat;

            health = party.Health;
            stamina = party.Stamina;

            charName = data.charName;

            //expAtLv = combat.GetStatAtLv(StatID.ExpAtLv, data.baseExpAtLv);
            
            vitAtLv = combat.GetStatAtLv(StatID.Vitality, data.baseVit);
            endAtLv = combat.GetStatAtLv(StatID.Endurance, data.baseEnd);

            atkAtLv = combat.GetStatAtLv(StatID.Attack, data.baseAtk);
            defAtLv = combat.GetStatAtLv(StatID.Defence, data.baseDef);

            hRegenAtLv = combat.GetStatAtLv(StatID.HealthRegen, data.baseHealthRegen);
            sRegenAtLv = combat.GetStatAtLv(StatID.StaminaRegen, data.baseStaminaRegen);

            //this.exp = exp;

            //level = CalculateLv();

            vitality = new Stat(vitAtLv[level - 1], 255);
            endurance = new Stat(endAtLv[level - 1], 255);

            attack = new Stat(atkAtLv[level - 1], 255);
            defence = new Stat(defAtLv[level - 1], 255);

            weight = new Stat(data.weight, 255);

            healthRegen = new Stat(hRegenAtLv[level - 1], 15);
            staminaRegen = new Stat(sRegenAtLv[level - 1], 63);

            skillset = data.actions.ToList();
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
            if (!skillset.Contains(skill1) || !skillset.Contains(skill2)) return;

            var i1 = skillset.IndexOf(skill1);
            var i2 = skillset.IndexOf(skill2);

            skillset.RemoveAt(i1);
            skillset.Insert(i1, skill2);

            skillset.RemoveAt(i2);
            skillset.Insert(i2, skill1);
        }
    }
}