using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AwesomeGame.PlayerMgmt
{
    public class EnemyTestStatistic : EntityStatistic<EnemyTestStatistic>
    {
        static EnemyTestStatistic( ) {
            string path = "./Assets/EnemyStats/" + MethodBase.GetCurrentMethod( ).DeclaringType.Name;
            if( !Directory.Exists( path ) )
                Directory.CreateDirectory( path );
            InitBaseStats( path + "/pos" );
        }
    }
}
