using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeGame.PlayerMgmt
{
    public class HealthModifier : Modifier
    {
        public float MaxHealth { get; private set; }
        public float HealthRestoredPerSecond { get; private set; }

        public HealthModifier( float maxHealth = 0.0f, float healthRestoredPerSecond = 0.0f, float time = -1.0f ) : base( time ) {
            MaxHealth = maxHealth;
            HealthRestoredPerSecond = healthRestoredPerSecond;
        }
    }
}
