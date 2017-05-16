using System.Collections;
using System.Collections.Generic;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class EntityStatistic : MonoBehaviour
    {
        public Dictionary<WheelPosition, AttackStatistic> AttackStats { get; protected set; }
        public Dictionary<WheelPosition, DefenseStatistic> DefenseStats { get; protected set; }

        protected virtual void Start( ) {
            AttackStats = new Dictionary<WheelPosition, AttackStatistic>( );
            DefenseStats = new Dictionary<WheelPosition, DefenseStatistic>( );
        }

        protected void InitStats( ) {
            //starting point for gui tool - DO NOT DELETE

            //ending point for gui tool - DO NOT DELETE
        }
    }
}

