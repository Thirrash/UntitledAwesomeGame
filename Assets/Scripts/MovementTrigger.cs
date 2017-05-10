using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AwesomeGame.EventMgmt
{
    public class MovementTrigger : MonoBehaviour
    {
        public delegate void AxisInput( float val );
        public static MovementTrigger Instance { get; private set; }

        public event AxisInput MoveHorizontal;
        public event AxisInput MoveVertical;
        public event AxisInput MoveZoom;
        public event AxisInput MoveMouseX;
        public event AxisInput MoveMouseY;

        void Start( ) {
            if( Instance != null )
                Destroy( this );
            else
                Instance = this;
        }

        void Update( ) {
            CheckMovement( );
        }

        void CheckMovement( ) {
            float tmp = 0.0f;

            if( ( tmp = Input.GetAxis( "Horizontal" ) ) != 0.0f )
                MoveHorizontal.Invoke( tmp );

            if( ( tmp = Input.GetAxis( "Vertical" ) ) != 0.0f )
                MoveVertical.Invoke( tmp );

            if( ( tmp = Input.GetAxis( "Mouse ScrollWheel" ) ) != 0.0f )
                MoveZoom.Invoke( tmp );

            if( ( tmp = Input.GetAxis( "Mouse X" ) ) != 0.0f )
                MoveMouseX.Invoke( tmp );

            if( ( tmp = Input.GetAxis( "Mouse Y" ) ) != 0.0f )
                MoveMouseY.Invoke( tmp );
        }
    }
}
