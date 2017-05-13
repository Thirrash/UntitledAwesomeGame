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

        void Start( ) {
            DamageModifier = 1.0f;
            WheelPos = (WheelPosition) Enum.Parse( typeof( WheelPosition ), gameObject.name );
            Wheel.Instance.AddAttackPosition( WheelPos, this );
        }
    }
}
