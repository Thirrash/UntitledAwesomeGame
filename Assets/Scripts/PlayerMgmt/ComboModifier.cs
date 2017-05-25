using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwesomeGame.UtilityMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class ComboModifier : Modifier
    {
        public float MinMultiplier { get; private set; }
        public float MaxMultiplier { get; private set; }
        public float ComboRestorePerSecond { get; private set; }
        public float ComboDimnishPerSecond { get; private set; }
        public Vector2 ComboGenerationForAttack { get; private set; }
        public Vector2 ComboGenerationForDefense { get; private set; }

        public ComboModifier( float minMultiplier = 0.0f, 
                              float maxMultiplier = 0.0f, 
                              float comboRestorePerSecond = 0.0f,
                              float comboDimnishPerSecond = 0.0f, 
                              Vector2 comboGenerationForAttack = default( Vector2 ),
                              Vector2 comboGenerationForDefense = default( Vector2 ), 
                              float time = -1.0f ) : base( time ) {
            MinMultiplier = minMultiplier;
            MaxMultiplier = maxMultiplier;
            ComboRestorePerSecond = comboRestorePerSecond;
            ComboDimnishPerSecond = comboDimnishPerSecond;
            ComboGenerationForAttack = comboGenerationForAttack;
            ComboGenerationForDefense = comboGenerationForDefense;
        }
    }
}
