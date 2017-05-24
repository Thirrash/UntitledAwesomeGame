using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class PlayerStatistic : EntityStatistic
    {
        protected override void Start( ) {
            base.Start( );

            foreach( WheelPosition pos in (WheelPosition[])Enum.GetValues( typeof( WheelPosition ) ) ) {
                if( pos == WheelPosition.Neutral )
                    continue;
                AttackStats.Add( pos, new AttackStatistic( ) );
                DefenseStats.Add( pos, new DefenseStatistic( ) );
            }
        }
    }
}
