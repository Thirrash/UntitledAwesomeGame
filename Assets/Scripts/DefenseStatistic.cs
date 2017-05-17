using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    [Serializable]
    public class DefenseStatistic : ICloneable
    {
        public float BaseDmgMultiplier = 1.0f;
        public float ArmorDmgMultiplier = 1.0f;

        public DefenseStatistic( ) {

        }

        public System.Object Clone( ) {
            DefenseStatistic copyObj = (DefenseStatistic)this.MemberwiseClone( );
            return copyObj;
        }

        public float GetReduction( ) {
            return BaseDmgMultiplier * ArmorDmgMultiplier;
        }
    }
}

