using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AwesomeGame.EventMgmt;

namespace AwesomeGame.PlayerMgmt
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        float horizontalSensitivity = 0.0f;

        [SerializeField]
        float verticalSensitivity = 0.0f;

        void Start( ) {
            MovementTrigger.Instance.MoveHorizontal += MoveHorizontal;
            MovementTrigger.Instance.MoveVertical += MoveVertical;
        }

        void OnDestroy( ) {
            MovementTrigger.Instance.MoveHorizontal -= MoveHorizontal;
            MovementTrigger.Instance.MoveVertical -= MoveVertical;
        }

        void MoveHorizontal( float val ) {
            transform.Translate( new Vector3( val * horizontalSensitivity * Time.deltaTime, 0.0f, 0.0f ), Space.Self );
        }

        void MoveVertical( float val ) {
            transform.Translate( new Vector3( 0.0f, 0.0f, val * verticalSensitivity * Time.deltaTime ), Space.Self );
        }
    }
}

