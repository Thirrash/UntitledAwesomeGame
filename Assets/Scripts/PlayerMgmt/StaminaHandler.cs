using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwesomeGame.UtilityMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class StaminaHandler : IHandler
    {
        public float CurrentStamina {
            get { return currentStamina; }
            set { currentStamina = Mathf.Clamp( value, 0.0f, MaxStamina ); }
        }

        public float MaxStamina {
            get { return ( maxStamina > Constants.GlobalMinimumMaxStamina ) ? maxStamina : Constants.GlobalMinimumMaxStamina; }
            protected set { maxStamina = value; }
        }

        public float StaminaRestoredPerSecond {
            get { return StaminaRestoredPerSecond; }
            protected set { StaminaRestoredPerSecond = value; }
        }

        public List<StaminaModifier> Modifier { get; private set; }

        float currentStamina;
        float maxStamina = 100.0f;
        float staminaRestoredPerSecond = 5.0f;

        public StaminaHandler( ) {
            currentStamina = maxStamina;
            Modifier = new List<StaminaModifier>( );
        }

        public void UpdateHandler( float timeElapsed ) {
            foreach( StaminaModifier s in Modifier ) {
                if( s.ChangeTime( timeElapsed ) ) {
                    OnModifierListChange( true, s );
                    Modifier.Remove( s );
                }
            }

            CurrentStamina += StaminaRestoredPerSecond * timeElapsed;
        }

        public void AddModifier( StaminaModifier modifier ) {
            Modifier.Add( modifier );
            OnModifierListChange( false, modifier );
        }

        void OnModifierListChange( bool isBeingRemoved, StaminaModifier changing ) {
            float sign = ( isBeingRemoved ) ? -1.0f : 1.0f;
            MaxStamina += changing.MaxStamina * sign;
            StaminaRestoredPerSecond += changing.StaminaRestoredPerSecond * sign;
        }
    }
}
