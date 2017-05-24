using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AwesomeGame.UtilityMgmt;
using AwesomeGame.WheelMgmt;

namespace AwesomeGame.PlayerMgmt
{
    public class EnemyTestStatistic : EntityStatistic
    {
        static Dictionary<WheelPosition, AttackStatistic> attackStatsBase;
        static Dictionary<WheelPosition, DefenseStatistic> defenseStatsBase;

        static EnemyTestStatistic( ) {
            attackStatsBase = new Dictionary<WheelPosition, AttackStatistic>( );
            defenseStatsBase = new Dictionary<WheelPosition, DefenseStatistic>( );
        }

        public EnemyTestStatistic( ) {
            if( attackStatsBase.Count != 0 )
                return;

            string path = Constants.StatsBasePath + "EnemyTest/";
            InitBaseStats( attackStatsBase, defenseStatsBase, path );
        }

        public override void TakeDamage( Dictionary<WheelPosition, AttackStatistic> attackDictionary, ComboHandler comboMultiplier ) {
            base.TakeDamage( attackDictionary, comboMultiplier );
            WheelPosition first = WheelPosition.InnerLeft;
            WheelPosition second = WheelPosition.OuterLeft;
            
            float maxNonComboDamage = 0.0f;
            float actualNonComboDamage = 0.0f;
            foreach( KeyValuePair<WheelPosition, AttackStatistic> k in attackDictionary ) {
                maxNonComboDamage += k.Value.GetBoost( );
            } 
        }
    }
}
