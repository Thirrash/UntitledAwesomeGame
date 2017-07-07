using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AwesomeGame.GuiMgmt
{
    public class CursorLock : MonoBehaviour
    {
        public GameObject CursorCenteredObj;
        public Texture2D CursorImage;
        public static CursorLock Instance { get; private set; }

        void Start( ) {
            if( Instance != null )
                Destroy( this );
            else
                Instance = this;

            Cursor.SetCursor( CursorImage, Vector2.zero, CursorMode.Auto );
            LockCursor( );
        }

        public void LockCursor( ) {
            Cursor.lockState = CursorLockMode.Locked;
            CursorCenteredObj.SetActive( true );
        }

        public void UnlockCursor( ) {
            Cursor.lockState = CursorLockMode.None;
            CursorCenteredObj.SetActive( false );
        }
    }
}
