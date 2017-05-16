using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AwesomeGame.WheelMgmt
{
    public class AttackPosition : MonoBehaviour
    {
        public WheelPosition WheelPos { get; private set; }
        public float DamageModifier { get; private set; }
        Wheel wheel;

        void Start( ) {
            wheel = (Wheel)Wheel.Instance;
            DamageModifier = 1.0f;
            WheelPos = (WheelPosition) Enum.Parse( typeof( WheelPosition ), gameObject.name );
            wheel.AddAttackPosition( WheelPos, this );
        }
    }
}
