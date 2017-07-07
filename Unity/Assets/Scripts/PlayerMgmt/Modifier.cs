using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeGame.PlayerMgmt
{
    public class Modifier
    {
        public float Time { get; protected set; }
        public bool IsPermanent { get; protected set; }

        public Modifier( float time = -1.0f ) {
            IsPermanent = ( time < 0.0f ) ? true : false;
            Time = time;
        }

        /// <summary>
        /// Change timer value
        /// </summary>
        /// <param name="Subtracted amount"></param>
        /// <returns>Whether timer reached 0.0f</returns>
        public bool ChangeTime( float value ) {
            if( IsPermanent )
                return false;

            Time -= value;
            return ( Time > 0.0f ) ? false : true;
        }
    }
}
