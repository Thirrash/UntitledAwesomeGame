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

        [SerializeField]
        float jumpForce = 0.0f;

        [SerializeField]
        float timeBetweenJumps = 1.5f;

        bool isAbleToJump = true;
        Rigidbody rigid;

        void Start( ) {
            rigid = GetComponent<Rigidbody>( );
            MovementTrigger.Instance.MoveHorizontal += MoveHorizontal;
            MovementTrigger.Instance.MoveVertical += MoveVertical;
            MovementTrigger.Instance.Jump += Jump;
        }

        void OnDestroy( ) {
            MovementTrigger.Instance.MoveHorizontal -= MoveHorizontal;
            MovementTrigger.Instance.MoveVertical -= MoveVertical;
            MovementTrigger.Instance.Jump -= Jump;
        }

        void MoveHorizontal( float val ) {
            transform.Translate( new Vector3( val * horizontalSensitivity * Time.deltaTime, 0.0f, 0.0f ), Space.Self );
        }

        void MoveVertical( float val ) {
            transform.Translate( new Vector3( 0.0f, 0.0f, val * verticalSensitivity * Time.deltaTime ), Space.Self );
        }

        void Jump( ) {
            if( !isAbleToJump )
                return;

            rigid.AddForce( new Vector3( 0.0f, jumpForce, 0.0f ), ForceMode.Impulse );
            StartCoroutine( WaitBetweenJumps( ) );
        }

        IEnumerator WaitBetweenJumps( ) {
            isAbleToJump = false;
            yield return new WaitForSecondsRealtime( timeBetweenJumps );
            isAbleToJump = true;
        }
    }
}

