using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwesomeGame.UtilityMgmt;
using UnityEngine;

namespace AwesomeGame.EventMgmt
{
    public class MovementTrigger : MonoBehaviour
    {
        public delegate void ClickInput( );
        public delegate void AxisInput( float val );
        public static MovementTrigger Instance { get; private set; }

        public event AxisInput MoveHorizontal;
        public event AxisInput MoveVertical;
        public event ClickInput Jump;
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
            bool tmpBool;

            if( ( tmp = Input.GetAxis( Constants.MoveHorizontalInput ) ) != 0.0f )
                MoveHorizontal.Invoke( tmp );

            if( ( tmp = Input.GetAxis( Constants.MoveVerticalInput ) ) != 0.0f )
                MoveVertical.Invoke( tmp );

            if( tmpBool = Input.GetButtonDown( Constants.JumpInput ) )
                Jump.Invoke( );

            if( ( tmp = Input.GetAxis( Constants.MouseWheelInput ) ) != 0.0f )
                MoveZoom.Invoke( tmp );

            if( ( tmp = Input.GetAxis( Constants.MouseXInput ) ) != 0.0f )
                MoveMouseX.Invoke( tmp );

            if( ( tmp = Input.GetAxis( Constants.MouseYInput ) ) != 0.0f )
                MoveMouseY.Invoke( tmp );
        }
    }
}
