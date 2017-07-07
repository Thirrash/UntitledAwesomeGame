﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwesomeGame.UtilityMgmt;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class PlayerStatistic : EntityStatistic
    {
        protected override void Start( ) {
            base.Start( );
            AttackStats = new Dictionary<WheelPosition, AttackStatistic>( );
            DefenseStats = new Dictionary<WheelPosition, DefenseStatistic>( );

            foreach( WheelPosition pos in (WheelPosition[])Enum.GetValues( typeof( WheelPosition ) ) ) {
                if( pos == WheelPosition.Neutral )
                    continue;
                AttackStats.Add( pos, new AttackStatistic( ) );
                DefenseStats.Add( pos, new DefenseStatistic( ) );
            }
        }
    }
}