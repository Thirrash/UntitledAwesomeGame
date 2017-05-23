using System;
using System.Collections;
using System.Collections.Generic;
using AwesomeGame.UtilityMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class ComboHandler
    {
        public float CurrentMultiplier {
            get { return currentMultiplier; }
            set { currentMultiplier = Mathf.Clamp( value, minMultiplier, maxMultiplier ); }
        }

        public float MinMultiplier {
            get { return minMultiplier; }
            set {
                if( value > currentMultiplier ) currentMultiplier = value;
                minMultiplier = ( value < 0.0f ) ? 0.0f : value;
            }
        }

        public float MaxMultiplier {
            get { return maxMultiplier; }
            set {
                if( value < currentMultiplier ) currentMultiplier = value;
                maxMultiplier = ( value < 0.0f ) ? 0.0f : value;
            }
        }

        public float NeutralMultiplier {
            get { return neutralMultiplier; }
            set { neutralMultiplier = Mathf.Clamp( value, minMultiplier, maxMultiplier ); }
        }

        public float ComboRestorePerSecond {
            get { return comboRestorePerSecond; }
            set { comboRestorePerSecond = ( value > 0.0f ) ? value : 0.0f; }
        }

        public float ComboDimnishPerSecond {
            get { return comboDimnishPerSecond; }
            set { comboDimnishPerSecond = ( value > 0.0f ) ? value : 0.0f; }
        }

        public List<ComboModifier> DebuffModifiers { get; private set; }
        public List<ComboModifier> BuffModifiers { get; private set; }

        float currentMultiplier = 1.0f;
        float minMultiplier = 0.5f;
        float maxMultiplier = 2.0f;
        float neutralMultiplier = 1.0f;
        float comboRestorePerSecond = 0.1f;
        float comboDimnishPerSecond = 0.1f;

        public ComboHandler( ) {
            DebuffModifiers = new List<ComboModifier>( );
            BuffModifiers = new List<ComboModifier>( );
        }

        public void AddModifier( ComboModifier modifier ) {
            if( modifier.Value < 0.0f ) {
                DebuffModifiers.Add( modifier );
                DebuffModifiers.Sort( ( ComboModifier x, ComboModifier y ) => x.Value.CompareTo( y.Value ) );
            } else {
                BuffModifiers.Add( modifier );
                BuffModifiers.Sort( ( ComboModifier x, ComboModifier y ) => -x.Value.CompareTo( y.Value ) );
            }
        }

        public void UpdateCombo( float timeElapsed ) {
            foreach( ComboModifier mod in DebuffModifiers ) {
                if( mod.ChangeTime( timeElapsed ) ) {
                    DebuffModifiers.Remove( mod );
                    //UPDATE GUI ELEMENT
                }
            }

            foreach( ComboModifier mod in BuffModifiers ) {
                if( mod.ChangeTime( timeElapsed ) ) {
                    BuffModifiers.Remove( mod );
                    //UPDATE GUI ELEMENT
                }
            }

            if( CurrentMultiplier > NeutralMultiplier )
                CurrentMultiplier -= ComboDimnishPerSecond * timeElapsed;
            else if( CurrentMultiplier < NeutralMultiplier )
                CurrentMultiplier += ComboRestorePerSecond * timeElapsed;
        }

        public float GetActualModifier( ) {
            return Mathf.Clamp( CurrentMultiplier + GetLowestDebuffModifier( ) + GetHighestBuffModifier( ), MinMultiplier, MaxMultiplier );
        }

        float GetLowestDebuffModifier( ) {
            return ( DebuffModifiers.Count > 0 ) ? DebuffModifiers[0].Value : 0.0f;
        }

        float GetHighestBuffModifier( ) {
            return ( BuffModifiers.Count > 0 ) ? BuffModifiers[0].Value : 0.0f;
        }
    }
}

