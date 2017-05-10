using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AwesomeGame.EventMgmt;

namespace AwesomeGame.PlayerMgmt
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float turnSensitivity;
        [SerializeField] float moveSensitivity;

        void Start( )
        {
            GlobalEvent.MoveHorizontal += MoveHorizontal;
            GlobalEvent.MoveVertical += MoveVertical;
        }

        void MoveHorizontal( float val )
        {
            transform.Rotate(new Vector3(0.0f, val * turnSensitivity, 0.0f));
        }

        void MoveVertical( float val )
        {
            transform.Translate(new Vector3(val, 0.0f, val * moveSensitivity), Space.Self);
        }
    }
}

