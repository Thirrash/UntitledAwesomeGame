using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AwesomeGame.PlayerMgmt;
using AwesomeGame.WheelMgmt;
using UnityEngine;
using UnityEngine.UI;

namespace AwesomeGame.EditorMgmt
{
    public class StatisticCreation : MonoBehaviour
    {
        public static StatisticCreation Instance { get; private set; }
        public EnemyType CurrentEnemyType;
        public bool IsDefenseMode = true;

        void Start( ) {
            if( Instance == null )
                Instance = this;
            else
                Destroy( this );
        }

        public void SaveCurrent( ) {
            foreach( WheelPosition pos in WheelEditor.Instance.Selected ) {
                string json = "";
                if( IsDefenseMode ) {
                    DefenseStatistic stat = new DefenseStatistic( );
                    json = JsonUtility.ToJson( stat );
                } else {
                    AttackStatistic stat = new AttackStatistic( );
                    json = JsonUtility.ToJson( stat );
                }

                using( FileStream stream = new FileStream( "./Assets/EnemyStats/" + CurrentEnemyType.ToString( ) + "Statistic/" +
                    ( ( IsDefenseMode ) ? "_defense_" : "_attack_" ) + pos.ToString( ), FileMode.Truncate, FileAccess.Write, FileShare.None ) )
                using( StreamWriter writer = new StreamWriter( stream ) )
                    writer.Write( json );
            }
        }
    }
}
