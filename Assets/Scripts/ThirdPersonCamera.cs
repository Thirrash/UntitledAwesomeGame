using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AwesomeGame.CameraMgmt
{
    public sealed class ThirdPersonCamera : CameraType
    {
        protected override void Start()
        {
            base.Start();
            FollowPlayer();
        }

        protected override void MoveHorizontal(float val)
        {
            base.MoveHorizontal(val);
            FollowPlayer();
        }

        protected override void MoveVertical(float val)
        {
            base.MoveVertical(val);
            FollowPlayer();
        }

        protected override void Zoom(float val)
        {
            base.Zoom(val);
            DistanceFromPlayer = DistanceFromPlayer + val;
            FollowPlayer();
        }

        void FollowPlayer( )
        {
            transform.rotation = Quaternion.Euler(PointOfView, Player.transform.rotation.eulerAngles.y, 0.0f);
            transform.position = Player.transform.position;
            transform.Translate(new Vector3(0.0f, 0.0f, -DistanceFromPlayer), Space.Self);
        }
    }
}
