using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwesomeGame.UtilityMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class HealthHandler : IHandler
    {
        public event Constants.BasicEventType OnDie;
        public event Constants.BasicEventType OnCurrentChange = delegate { };

        public float CurrentHealth {
            get { return currentHealth; }
            set {
                currentHealth = Mathf.Clamp( value, -0.1f, MaxHealth );
                OnCurrentChange.Invoke( );
                if( currentHealth <= 0.0f )
                    OnDie.Invoke( );
            }
        }

        public float MaxHealth {
            get { return ( maxHealth > Constants.GlobalMinimumMaxHealth ) ? maxHealth : Constants.GlobalMinimumMaxHealth; }
            protected set { maxHealth = value; }
        }

        public float HealthRestoredPerSecond {
            get { return healthRestoredPerSecond; }
            protected set { healthRestoredPerSecond = value; }
        }

        public List<HealthModifier> Modifier { get; private set; }

        float currentHealth;
        float maxHealth = 100.0f;
        float healthRestoredPerSecond = 0.5f;

        public HealthHandler( ) {
            currentHealth = maxHealth;
            Modifier = new List<HealthModifier>( );
        }

        public void UpdateHandler( float timeElapsed ) {
            foreach( HealthModifier h in Modifier ) {
                if( h.ChangeTime( timeElapsed ) ) {
                    OnModifierListChange( true, h );
                    Modifier.Remove( h );
                }
            }

            CurrentHealth += HealthRestoredPerSecond * timeElapsed;
        }

        public void AddModifier( HealthModifier modifier ) {
            Modifier.Add( modifier );
            OnModifierListChange( false, modifier );
        }

        void OnModifierListChange( bool isBeingRemoved, HealthModifier changing ) {
            float sign = ( isBeingRemoved ) ? -1.0f : 1.0f;
            MaxHealth += changing.MaxHealth * sign;
            HealthRestoredPerSecond += changing.HealthRestoredPerSecond * sign;
        }
    }
}
