using System;
using System.Collections;
using System.Collections.Generic;
using AwesomeGame.PlayerMgmt;
using UnityEngine;
using UnityEngine.UI;

namespace AwesomeGame.GuiMgmt
{
    public class PlayerBasic : MonoBehaviour
    {
        [SerializeField]
        protected EntityStatistic stats;

        [SerializeField]
        protected Image healthBar;
        [SerializeField]
        protected Text healthText;

        [SerializeField]
        protected Image staminaBar;
        [SerializeField]
        protected Text staminaText;

        [SerializeField]
        protected Image comboMinusBar;
        [SerializeField]
        protected Image comboPlusBar;
        [SerializeField]
        protected Text comboText;

        protected virtual void Start( ) {
            stats.HealthHandler.OnCurrentChange += OnHealthChange;
            stats.StaminaHandler.OnCurrentChange += OnStaminaChange;
            stats.ComboHandler.OnCurrentChange += OnComboChange;
            OnHealthChange( );
            OnStaminaChange( );
            OnComboChange( );
        }

        protected void OnHealthChange( ) {
            healthText.text = (int)stats.HealthHandler.CurrentHealth + " / " + (int)stats.HealthHandler.MaxHealth;
            healthBar.fillAmount = stats.HealthHandler.CurrentHealth / stats.HealthHandler.MaxHealth;
        }

        protected void OnStaminaChange( ) {
            staminaText.text = (int)stats.StaminaHandler.CurrentStamina + " / " + (int)stats.StaminaHandler.MaxStamina;
            staminaBar.fillAmount = stats.StaminaHandler.CurrentStamina / stats.StaminaHandler.MaxStamina;
        }

        protected void OnComboChange( ) {
            float neutralCombo = stats.ComboHandler.MinMultiplier + ( stats.ComboHandler.MaxMultiplier - stats.ComboHandler.MinMultiplier ) / 2.0f;
            comboText.text = stats.ComboHandler.CurrentMultiplier.ToString( "f1" ) + " / " + stats.ComboHandler.MaxMultiplier.ToString( "f1" );
            if( stats.ComboHandler.CurrentMultiplier > neutralCombo ) {
                comboMinusBar.enabled = false;
                comboPlusBar.enabled = true;
                comboPlusBar.fillAmount = ( stats.ComboHandler.CurrentMultiplier - neutralCombo ) / ( stats.ComboHandler.MaxMultiplier - neutralCombo );
            } else {
                comboMinusBar.enabled = true;
                comboPlusBar.enabled = false;
                comboMinusBar.fillAmount = ( neutralCombo - stats.ComboHandler.CurrentMultiplier ) / ( neutralCombo - stats.ComboHandler.MinMultiplier );
            }
        }
    }
}

