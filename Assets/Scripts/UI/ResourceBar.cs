using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG_Project
{
    public class ResourceBar : MonoBehaviour, IBattleUIElement
    {
        [SerializeField] protected PartyController party;
        [SerializeField] protected Resource resource;

        [SerializeField] protected Image background;
        [SerializeField] protected Image fillShadow;
        [SerializeField] protected Image fill;

        [SerializeField] protected Text text;

        [SerializeField] float shadowSpeed = 0.25f;

        protected string textHeader;

        string Text => textHeader + resource.ResourcePointValue + "/" + 
            resource.ResourceStatValue;

        public virtual void InitUI(PartyController party)
        {
            this.party = party;

            fillShadow.fillAmount = resource.ResourceFraction;
            fill.fillAmount = resource.ResourceFraction;

            SubscribeToDelegates();
        }

        public virtual void SubscribeToDelegates()
        {

        }

        public virtual void UnsubscribeFromDelegates()
        {

        }

        public virtual void UpdateUI()
        {
            fill.fillAmount = resource.ResourceFraction;

            fillShadow.fillAmount = Mathf.MoveTowards(fillShadow.fillAmount, 
                fill.fillAmount, shadowSpeed * Time.deltaTime);

            text.text = Text;
        }
    }
}