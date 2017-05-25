using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeGame.PlayerMgmt
{
    public class StaminaModifier : Modifier
    {
        public float MaxStamina { get; private set; }
        public float StaminaRestoredPerSecond { get; private set; }

        public StaminaModifier( float maxStamina = 0.0f, float staminaRestoredPerSecond = 0.0f, float time = -1.0f ) : base( time ) {
            MaxStamina = maxStamina;
            StaminaRestoredPerSecond = staminaRestoredPerSecond;
        }
    }
}
