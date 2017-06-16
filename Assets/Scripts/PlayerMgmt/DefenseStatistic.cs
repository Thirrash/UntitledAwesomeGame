using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    [Serializable]
    public class DefenseStatistic : ICloneable
    {
        public float BaseDmgReduction = 1.0f;
        public float ArmorDmgReduction = 1.0f;

        public DefenseStatistic( ) {

        }

        public System.Object Clone( ) {
            DefenseStatistic copyObj = (DefenseStatistic)this.MemberwiseClone( );
            return copyObj;
        }

        public float GetReduction( ) {
            return BaseDmgReduction;
        }

        public float GetReduction( WheelPosition attackPos, WheelPosition defPos ) {
            float distanceModifier = 1.0f - WheelLink.GetDistance( attackPos, defPos ) / WheelLink.MaximumDistance;
            return BaseDmgReduction + ( ArmorDmgReduction ) * distanceModifier;
        }
    }
}

