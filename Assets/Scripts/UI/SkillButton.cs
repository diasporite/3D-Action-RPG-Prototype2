using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class SkillButton : MonoBehaviour
    {
        [field: SerializeField] public ActionData Skill { get; set; }

        [SerializeField] Text actionText;
        [SerializeField] Text spText;
        [SerializeField] Text apText;

        public void UpdateUI()
        {
            if (Skill != null)
            {
                actionText.text = Skill.name;

                spText.text = "SP " + Skill.SpCost;
                apText.text = "AP " + Skill.MpCost;
            }
            else
            {
                actionText.text = "";

                spText.text = "SP --";
                apText.text = "AP --";
            }
        }
    }
}