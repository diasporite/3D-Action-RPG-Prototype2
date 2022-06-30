using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class PartyPanel : MonoBehaviour
    {
        [field: SerializeField] public CharData Character { get; set; }

        [SerializeField] Image portrait;

        [SerializeField] Text nameText;

        //[SerializeField] ResourceBar apBar;

        public void UpdateUI()
        {
            if (Character == null)
            {
                portrait.gameObject.SetActive(false);
                nameText.gameObject.SetActive(false);
            }
            else
            {
                portrait.gameObject.SetActive(true);
                nameText.gameObject.SetActive(true);

                portrait.sprite = Character.Portrait;
                nameText.text = Character.CharName;
            }
        }
    }
}