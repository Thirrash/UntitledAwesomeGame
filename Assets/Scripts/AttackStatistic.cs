using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class AttackStatistic : ScriptableObject, ICloneable
    {
        public float BaseDmgMultiplier;
        public float WeaponDmgMultiplier;

        public AttackStatistic( ) {

        }

        public System.Object Clone( ) {
            AttackStatistic copyObj = (AttackStatistic)this.MemberwiseClone( );
            return copyObj;
        }

        public float GetBoost( ) {
            return BaseDmgMultiplier * WeaponDmgMultiplier;
        }
    }
}

