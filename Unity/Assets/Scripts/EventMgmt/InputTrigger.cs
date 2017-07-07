using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwesomeGame.UtilityMgmt;
using UnityEngine;

namespace AwesomeGame.EventMgmt
{
    public class InputTrigger : MonoBehaviour
    {
        public delegate void ClickInput( );
        public static InputTrigger Instance { get; private set; }

        public event ClickInput MouseLeftButton;

        void Start( ) {
            if( Instance != null )
                Destroy( this );
            else
                Instance = this;
        }

        void Update( ) {
            CheckMouse( );
        }

        void CheckMouse( ) {
            float tmp = 0.0f;

            if( Input.GetButtonDown( Constants.MouseLeftInput ) )
                MouseLeftButton.Invoke( );
        }
    }
}
