using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AwesomeGame.UtilityMgmt;

namespace AwesomeGame.PlayerMgmt
{
    public class EnemyTestStatistic : EntityStatistic<EnemyTestStatistic>
    {
        public EnemyTestStatistic( ) {
            if( attackStatsBase.Count != 0 )
                return;

            string path = Constants.StatsBasePath + "EnemyTest/";
            InitBaseStats( path );
        }
    }
}
