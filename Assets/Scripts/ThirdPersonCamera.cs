using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AwesomeGame.CameraMgmt
{
    public sealed class ThirdPersonCamera : CameraBase
    {
        [SerializeField]
        float minPointOfView = 20.0f;

        [SerializeField]
        float maxPointOfView = 70.0f;

        [SerializeField]
        float offsetFromGround = 2.0f;

        protected override void Start( ) {
            base.Start( );
        }

        protected override void Update( ) {
            FollowPlayer( );
        }

        protected override void OnDestroy( ) {
            base.OnDestroy( );
        }

        protected override void MoveHorizontal( float val ) {
            base.MoveHorizontal( val );
            Player.transform.Rotate( new Vector3( 0.0f, val * horizontalSensitivity * Time.deltaTime ) );
        }

        protected override void MoveVertical( float val ) {
            base.MoveVertical( val );
            PointOfView = Mathf.Clamp( PointOfView - val * verticalSensitivity * Time.deltaTime, minPointOfView, maxPointOfView );
        }

        protected override void Zoom( float val ) {
            base.Zoom( val );
            transform.Translate( new Vector3( 0.0f, 0.0f, val * Time.deltaTime ), Space.Self );
        }

        void FollowPlayer( ) {
            transform.position = Player.transform.position;
            transform.rotation = Quaternion.Euler( new Vector3( PointOfView, Player.transform.rotation.eulerAngles.y, 0.0f ) );
            transform.Translate( new Vector3( 0.0f, 0.0f, -DistanceFromPlayer ), Space.Self );
            transform.position += new Vector3( 0.0f, offsetFromGround, 0.0f );
        }
    }
}
