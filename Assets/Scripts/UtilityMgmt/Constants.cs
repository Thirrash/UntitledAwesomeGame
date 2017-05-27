using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeGame.UtilityMgmt
{
    public static class Constants
    {
        public delegate void BasicEventType( );

        public static readonly int PlayerLayer = 9;
        public static readonly int EnemyLayer = 10;

        public static readonly string MoveHorizontalInput = "Horizontal";
        public static readonly string MoveVerticalInput = "Vertical";
        public static readonly string JumpInput = "Jump";
        public static readonly string MouseXInput = "Mouse X";
        public static readonly string MouseYInput = "Mouse Y";
        public static readonly string MouseWheelInput = "Mouse ScrollWheel";
        public static readonly string MouseLeftInput = "Fire1";

        public static readonly string StatsBasePath = "./Assets/EnemyStats/";

        public static readonly float GlobalMinimumComboMultiplier = 0.2f;
        public static readonly float GlobalMaximumComboMultiplier = 5.0f;
        public static readonly float GlobalMinimumComboDimnish = 0.01f;
        public static readonly float GlobalMinimumComboRegain = 0.01f;

        public static readonly float GlobalMinimumMaxHealth = 50.0f;
        public static readonly float GlobalMinimumMaxStamina = 50.0f;

        public static readonly float GlobalMinimumStaminaAttackModifier = 0.2f;
        public static readonly float GlobalMinimumStaminaDefenseModifier = 0.2f;
    }
}
