using AwesomeGame.EventMgmt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeGame.CameraMgmt
{
    public class CameraType : MonoBehaviour
    {
        public GameObject Player;
        public float DistanceFromPlayer;
        public float PointOfView;

        [SerializeField] protected float horizontalSensitivity;
        [SerializeField] protected float verticalSensitivity;
        [SerializeField] protected float zoomSensitivity;
        protected bool isToFollowPlayer;
        protected Camera camera;

        protected virtual void Start( ) {
            camera = GetComponent<Camera>( );
            ToggleFollowPlayer( true );
        }

        public void ToggleFollowPlayer( bool isToFollow ) {
            if (isToFollowPlayer == isToFollow)
                return;

            if( isToFollow == true ) {
                GlobalEvent.MoveHorizontal += MoveHorizontal;
                GlobalEvent.MoveVertical += MoveVertical;
                GlobalEvent.MoveZoom += Zoom;
            } else {
                GlobalEvent.MoveHorizontal -= MoveHorizontal;
                GlobalEvent.MoveVertical -= MoveVertical;
                GlobalEvent.MoveZoom -= Zoom;
            }

            isToFollowPlayer = isToFollow;
        }

        protected virtual void MoveHorizontal( float val ) {

        }

        protected virtual void MoveVertical( float val ) {

        }

        protected virtual void Zoom( float val ) {

        }
    }
}

