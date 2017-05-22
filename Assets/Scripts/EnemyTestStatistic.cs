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
    }
}
