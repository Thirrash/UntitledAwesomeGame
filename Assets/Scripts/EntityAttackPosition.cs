using System;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class EntityAttackPosition : MonoBehaviour
    {
        public WheelPosition WheelPos { get; private set; }

        protected virtual void Start( ) {
            WheelPos = (WheelPosition)Enum.Parse( typeof( WheelPosition ), gameObject.name );
            GetComponentInParent<EntityBehaviour>( ).AddPosition( WheelPos, this );
        }
    }
}