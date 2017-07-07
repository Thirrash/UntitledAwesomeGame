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
            string path = Constants.StatsBasePath + "EnemyTest/";
            InitBaseStats( attackStatsBase, defenseStatsBase, path );
        }

        public EnemyTestStatistic( ) {
            AttackStats = attackStatsBase.CloneDictionaryCloningValues( );
            DefenseStats = defenseStatsBase.CloneDictionaryCloningValues( );
        }

        protected override void Start( ) {
            base.Start( );
        }

        public override void TakeDamage( List<WheelPosition> attackPosition, List<AttackStatistic> attack, List<WheelPosition> defensePosition, ComboHandler comboMultiplier, float staminaAttackModifier ) {
            base.TakeDamage( attackPosition, attack, defensePosition, comboMultiplier, staminaAttackModifier );
        }
    }
}
