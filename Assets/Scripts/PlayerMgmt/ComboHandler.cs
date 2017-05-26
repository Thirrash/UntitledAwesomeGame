using System;
using System.Collections;
using System.Collections.Generic;
using AwesomeGame.UtilityMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class ComboHandler : IHandler
    {
        public event Constants.BasicEventType OnCurrentChange = delegate { };

        public float CurrentMultiplier {
            get { return currentMultiplier; }
            private set {
                currentMultiplier = Mathf.Clamp( value, MinMultiplier, MaxMultiplier );
                OnCurrentChange.Invoke( );
            }
        }

        public float MinMultiplier {
            get {
                return Mathf.Clamp( minMultiplier, 
                                    Constants.GlobalMinimumComboMultiplier, 
                                    Mathf.Max( maxMultiplier, Constants.GlobalMaximumComboMultiplier ) );
            } private set { minMultiplier = value; }
        }

        public float MaxMultiplier {
            get {
                return Mathf.Clamp( maxMultiplier,
                                    Mathf.Max( minMultiplier, Constants.GlobalMinimumComboMultiplier ), 
                                    Constants.GlobalMaximumComboMultiplier );
            } private set { maxMultiplier = value; }
        }

        public float ComboRestorePerSecond {
            get { return ( comboRestorePerSecond > Constants.GlobalMinimumComboRegain ) ? comboRestorePerSecond : Constants.GlobalMinimumComboRegain; ; }
            private set { comboRestorePerSecond = value; }
        }

        public float ComboDimnishPerSecond {
            get { return ( comboDimnishPerSecond > Constants.GlobalMinimumComboDimnish ) ? comboDimnishPerSecond : Constants.GlobalMinimumComboDimnish; }
            private set { comboDimnishPerSecond = value; }
        }

        public Vector2 ComboGenerationForAttack {
            get {
                return new Vector2( ( comboGenerationForAttack.x <= 0.0f ) ? comboGenerationForAttack.x : 0.0f,
                                    ( comboGenerationForAttack.y >= 0.0f ) ? comboGenerationForAttack.y : 0.0f );
            } private set { comboGenerationForAttack = value; }
        }

        public Vector2 ComboGenerationForDefense {
            get {
                return new Vector2( ( comboGenerationForDefense.x <= 0.0f ) ? comboGenerationForDefense.x : 0.0f,
                                    ( comboGenerationForDefense.y >= 0.0f ) ? comboGenerationForDefense.y : 0.0f );
            }
            private set { comboGenerationForDefense = value; }
        }

        public List<ComboModifier> Modifier { get; private set; }

        float currentMultiplier = 1.0f;
        float minMultiplier = 0.5f;
        float maxMultiplier = 1.5f;
        float comboRestorePerSecond = 0.1f;
        float comboDimnishPerSecond = 0.1f;
        Vector2 comboGenerationForAttack = new Vector2( -0.6f, 0.6f );
        Vector2 comboGenerationForDefense = new Vector2( -0.4f, 0.4f );

        public ComboHandler( ) {
            Modifier = new List<ComboModifier>( );
        }

        public void UpdateHandler( float timeElapsed ) {
            foreach( ComboModifier c in Modifier ) {
                if( c.ChangeTime( timeElapsed ) ) {
                    OnModifierListChange( true, c );
                    Modifier.Remove( c );
                }
            }

            float neutralMultiplier = ( MaxMultiplier + MinMultiplier ) / 2.0f;
            if( CurrentMultiplier > neutralMultiplier ) {
                float change = ComboDimnishPerSecond * timeElapsed;
                CurrentMultiplier = ( CurrentMultiplier - change < neutralMultiplier ) ? neutralMultiplier : CurrentMultiplier - change;
            } else if( CurrentMultiplier < neutralMultiplier ) {
                float change = ComboRestorePerSecond * timeElapsed;
                CurrentMultiplier = ( CurrentMultiplier + change > neutralMultiplier ) ? neutralMultiplier : CurrentMultiplier + change;
            }
        }

        public void ChangeCombo( float percentDamageDealt, bool isForAttack ) {
            float generationRange = ( isForAttack ) ? Math.Abs( ComboGenerationForAttack.x ) + Math.Abs( ComboGenerationForAttack.y ) :
                Math.Abs( ComboGenerationForDefense.x ) + Math.Abs( ComboGenerationForDefense.y );
            CurrentMultiplier += ( isForAttack ) ? ComboGenerationForAttack.x + generationRange * percentDamageDealt :
                ComboGenerationForDefense.x + generationRange * percentDamageDealt;
            Debug.Log( percentDamageDealt + " " + ( Math.Abs( ComboGenerationForAttack.x ) + generationRange * percentDamageDealt ) );
        }

        public void AddModifier( ComboModifier modifier ) {
            Modifier.Add( modifier );
            OnModifierListChange( false, modifier );
        }

        void OnModifierListChange( bool isBeingRemoved, ComboModifier changing ) {
            float sign = ( isBeingRemoved ) ? -1.0f : 1.0f;
            MinMultiplier += changing.MinMultiplier * sign;
            MaxMultiplier += changing.MaxMultiplier * sign;
            ComboRestorePerSecond += changing.ComboRestorePerSecond * sign;
            ComboDimnishPerSecond += changing.ComboDimnishPerSecond * sign;
            ComboGenerationForAttack += changing.ComboGenerationForAttack * sign;
            ComboGenerationForDefense += changing.ComboGenerationForDefense * sign;
            CurrentMultiplier = CurrentMultiplier;
        }
    }
}

