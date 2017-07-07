using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeGame.WheelMgmt
{
    public abstract class WheelBase : MonoBehaviour
    {
        public bool IsBlocked { get; protected set; }
        public bool IsClicked { get; protected set; }

        [SerializeField]
        protected GameObject wheelObj;

        protected Dictionary<WheelPosition, WheelFragment> fragments;
        public List<WheelPosition> Selected { get; protected set; }

        protected virtual void Start( ) {
            fragments = new Dictionary<WheelPosition, WheelFragment>( );
            Selected = new List<WheelPosition>( );
        }

        public abstract bool AddFragment( WheelPosition pos, WheelFragment fragment );
        public abstract bool ActivateFragment( WheelPosition pos );
        public abstract void ToggleActivation( bool isActive );
        public abstract void Finalize( bool isEarlyFinalize = false );
    }
}

