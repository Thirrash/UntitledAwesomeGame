using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class PlayerStatistic : MonoBehaviour
    {
        public Dictionary<WheelPosition, AttackStatistic> AttackStats { get; protected set; }
        public Dictionary<WheelPosition, DefenseStatistic> DefenseStats { get; protected set; }

        protected virtual void Start( ) {
            AttackStats = new Dictionary<WheelPosition, AttackStatistic>( );
            DefenseStats = new Dictionary<WheelPosition, DefenseStatistic>( );
        }
    }
}
