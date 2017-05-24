using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class AttackStatistic : ICloneable
    {
        public float BaseDmg = 2.0f;
        public float WeaponDmg = 2.0f;

        public AttackStatistic( ) {

        }

        public System.Object Clone( ) {
            AttackStatistic copyObj = (AttackStatistic)this.MemberwiseClone( );
            return copyObj;
        }

        public float GetBoost( ) {
            return BaseDmg + WeaponDmg;
        }
    }
}

