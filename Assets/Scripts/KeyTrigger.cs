using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AwesomeGame.EventMgmt
{
    public class KeyTrigger : MonoBehaviour
    {
        void Update()
        {
            CheckMovement();
        }

        void CheckMovement()
        {
            float tmp = 0.0f;

            if ((tmp = Input.GetAxis("Horizontal")) != 0.0f)
                GlobalEvent.MoveHorizontal(tmp);
        }
    }
}
