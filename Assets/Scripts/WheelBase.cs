using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeGame.WheelMgmt
{
    public abstract class WheelBase : MonoBehaviour
    {
        public static WheelBase Instance { get; protected set; }
        public bool IsBlocked { get; protected set; }
        public bool IsClicked { get; protected set; }

        [SerializeField]
        protected GameObject wheelObj;

        protected Dictionary<WheelPosition, WheelFragment> fragments = new Dictionary<WheelPosition, WheelFragment>( );
        protected List<WheelPosition> selected = new List<WheelPosition>( );

        protected virtual void Start( ) {
            if( Instance != null )
                Destroy( this );
            else
                Instance = this;
        }

        public abstract bool AddFragment( WheelPosition pos, WheelFragment fragment );
        public abstract bool ActivateFragment( WheelPosition pos );
        public abstract void ToggleActivation( bool isActive );
        public abstract void Finalize( bool isEarlyFinalize = false );
    }
}

