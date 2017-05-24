using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeGame.PlayerMgmt
{
    public class ComboModifier
    {
        public float Time { get; private set; }
        public float Value { get; private set; }

        public ComboModifier( float time, float value ) {
            Time = time;
            Value = value;
        }

        /// <summary>
        /// Change timer value
        /// </summary>
        /// <param name="Subtracted amount"></param>
        /// <returns>Whether timer reached 0.0f</returns>
        public bool ChangeTime( float value ) {
            Time -= value;
            return ( Time > 0.0f ) ? false : true;
        }
    }
}
