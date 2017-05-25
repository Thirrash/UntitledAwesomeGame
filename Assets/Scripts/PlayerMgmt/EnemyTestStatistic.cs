using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AwesomeGame.UtilityMgmt;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class EnemyTestStatistic : EntityStatistic
    {
        static Dictionary<WheelPosition, AttackStatistic> attackStatsBase;
        static Dictionary<WheelPosition, DefenseStatistic> defenseStatsBase;

        static EnemyTestStatistic( ) {
            attackStatsBase = new Dictionary<WheelPosition, AttackStatistic>( );
            defenseStatsBase = new Dictionary<WheelPosition, DefenseStatistic>( );
            Debug.Log( "Init EnemyTest" );
            string path = Constants.StatsBasePath + "EnemyTest/";
            InitBaseStats( attackStatsBase, defenseStatsBase, path );
        }

        public EnemyTestStatistic( ) {
            AttackStats = attackStatsBase.CloneDictionaryCloningValues( );
            DefenseStats = defenseStatsBase.CloneDictionaryCloningValues( );
            Debug.Log( "Copied count = " + DefenseStats.Count );
        }

        protected override void Start( ) {
            base.Start( );
            Debug.Log( HealthHandler.CurrentHealth );
        }

        public override void TakeDamage( List<WheelPosition> position, List<AttackStatistic> attack, ComboHandler comboMultiplier ) {
            base.TakeDamage( position, attack, comboMultiplier );
            List<WheelPosition> rolledPos = new List<WheelPosition> {
                WheelPosition.Center, WheelPosition.InnerLeft
            };

            float maxNonComboDamage = 0.0f;
            float actualNonComboDamage = 0.0f;
            for( int i = 0; i < position.Count; i++ ) {
                maxNonComboDamage += attack[i].GetBoost( );
                actualNonComboDamage += Mathf.Clamp( attack[i].GetBoost( ) -
                    ( ( rolledPos.Count > i ) ? DefenseStats[position[i]].GetReduction( position[i], rolledPos[i] ) : DefenseStats[position[i]].GetReduction( ) ),
                    0.0f, attack[i].GetBoost( ) );
                float percentDamageDealt = actualNonComboDamage / maxNonComboDamage;
                comboMultiplier.ChangeCombo( percentDamageDealt, true );
                Debug.Log( "Combo = " + comboMultiplier.CurrentMultiplier );
                ComboHandler.ChangeCombo( percentDamageDealt, false );
                Debug.Log( "Damage = " + actualNonComboDamage + " / " + maxNonComboDamage );
                HealthHandler.CurrentHealth -= actualNonComboDamage * comboMultiplier.CurrentMultiplier;
                Debug.Log( "HP = " + HealthHandler.CurrentHealth );
            }
        }
    }
}
