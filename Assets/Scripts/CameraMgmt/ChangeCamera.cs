using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AwesomeGame.CameraMgmt
{
    public static class ChangeCamera
    {
        public static Dictionary<CameraType, Camera> AvailableCameras { get; private set; }
        public static Camera ActiveCamera { get; private set; }

        static ChangeCamera( ) {
            AvailableCameras = new Dictionary<CameraType, Camera>( );
        }

        public static bool Add( CameraType type, Camera cam ) {
            if( AvailableCameras.ContainsKey( type ) )
                return false;

            AvailableCameras.Add( type, cam );
            return true;
        }

        public static bool Change( CameraType type ) {
            if( !AvailableCameras.ContainsKey( type ) )
                return false;

            ActiveCamera.gameObject.SetActive( false );
            ActiveCamera = AvailableCameras[type];
            ActiveCamera.gameObject.SetActive( true );
            return true;
        }
    }
}
