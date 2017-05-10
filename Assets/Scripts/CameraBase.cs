using AwesomeGame.EventMgmt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeGame.CameraMgmt
{
    public class CameraBase : MonoBehaviour
    {
        public CameraType Type;
        public GameObject Player;
        public float DistanceFromPlayer;
        public float PointOfView;

        [SerializeField]
        protected float horizontalSensitivity;

        [SerializeField]
        protected float verticalSensitivity;

        [SerializeField]
        protected float zoomSensitivity;
        protected Camera cameraComp;

        protected virtual void Start( ) {
            cameraComp = GetComponent<Camera>( );
            ChangeCamera.Add( Type, cameraComp );

            MovementTrigger.Instance.MoveMouseX += MoveHorizontal;
            MovementTrigger.Instance.MoveMouseY += MoveVertical;
            MovementTrigger.Instance.MoveZoom += Zoom;
        }

        protected virtual void Update( ) {

        }

        protected virtual void OnDestroy( ) {
            MovementTrigger.Instance.MoveMouseX -= MoveHorizontal;
            MovementTrigger.Instance.MoveMouseY -= MoveVertical;
            MovementTrigger.Instance.MoveZoom -= Zoom;
        }

        protected virtual void MoveHorizontal( float val ) {

        }

        protected virtual void MoveVertical( float val ) {

        }

        protected virtual void Zoom( float val ) {

        }
    }
}

