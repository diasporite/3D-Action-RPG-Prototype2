using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG_Project
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Combat/Character")]
    public class CharData : ScriptableObject
    {
        [field: Header("Info")]
        [field: SerializeField] public string CharName { get; private set; }
        [field: SerializeField] public Sprite Portrait { get; private set; }

        [field: Header("Resource Stats")]
        [field: SerializeField] public int BaseVit { get; private set; } = 50;
        [field: SerializeField] public int BaseEnd { get; private set; } = 50;

        [field: Header("Combat Stats")]
        [field: SerializeField] public int BaseAtk { get; private set; } = 50;
        [field: SerializeField] public int BaseDef { get; private set; } = 50;
        [field: Range(1, 255)]
        [field: SerializeField] public int Weight { get; private set; } = 128;

        [field: Header("Regen Stats")]
        [field: SerializeField] public int BaseHealthRegen { get; private set; } = 4;
        [field: SerializeField] public int BaseStaminaRegen { get; private set; } = 32;

        [field: Header("Actions")]
        [field: SerializeField] public ActionData[] Actions { get; private set; }
        [field: SerializeField] public ActionData DefendAction { get; private set; }

        public ActionData GetAction(int index)
        {
            index = Mathf.Abs(index);
            index = index % Actions.Length;

            return Actions[index];
        }
    }
}