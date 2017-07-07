using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AwesomeGame.PlayerMgmt;
using AwesomeGame.UtilityMgmt;
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

        public Dictionary<WheelPosition, AttackStatistic> AttackStats;
        public Dictionary<WheelPosition, DefenseStatistic> DefenseStats;

        [SerializeField]
        WheelEditor wheel;

        void Start( ) {
            if( Instance == null )
                Instance = this;
            else
                Destroy( this );

            AttackStats = new Dictionary<WheelPosition, AttackStatistic>( );
            DefenseStats = new Dictionary<WheelPosition, DefenseStatistic>( );
            ChangeEnemyType( EnemyType.EnemyTest );
        }

        public void ChangeEnemyType( EnemyType type ) {
            CurrentEnemyType = type;
            AttackStats.Clear( );
            DefenseStats.Clear( );

            foreach( WheelPosition pos in (WheelPosition[])Enum.GetValues( typeof( WheelPosition ) ) ) {
                if( pos == WheelPosition.Neutral )
                    continue;

                string json = "";
                using( FileStream stream = new FileStream( Constants.StatsBasePath + type.ToString( ) + "/_attack_" + pos.ToString( ) + ".awg", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read ) )
                using( StreamReader reader = new StreamReader( stream ) )
                    json = reader.ReadToEnd( );
                AttackStatistic attStats = JsonUtility.FromJson<AttackStatistic>( json );
                if( attStats == null ) attStats = new AttackStatistic( ); Debug.LogWarning( "Read null for attack " + pos.ToString( ) );
                AttackStats.Add( pos, attStats );


                using( FileStream stream = new FileStream( Constants.StatsBasePath + type.ToString( ) + "/_defense_" + pos.ToString( ) + ".awg", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read ) )
                using( StreamReader reader = new StreamReader( stream ) )
                    json = reader.ReadToEnd( );
                DefenseStatistic defStats = JsonUtility.FromJson<DefenseStatistic>( json );
                if( defStats == null ) defStats = new DefenseStatistic( ); Debug.LogWarning( "Read null for defense " + pos.ToString( ) );
                DefenseStats.Add( pos, defStats );
            }
        }

        public void SaveCurrent( ) {
            string dirPath = Constants.StatsBasePath + CurrentEnemyType.ToString( );
            if( !Directory.Exists( dirPath ) )
                Directory.CreateDirectory( dirPath );

            foreach( WheelPosition pos in wheel.Selected ) {
                string json = "";
                if( IsDefenseMode ) {
                    json = JsonUtility.ToJson( DefenseStats[pos] );
                } else {
                    json = JsonUtility.ToJson( AttackStats[pos] );
                }

                Debug.Log( json );
                using( FileStream stream = new FileStream( dirPath + "/" +
                    ( ( IsDefenseMode ) ? "_defense_" : "_attack_" ) + pos.ToString( ) + ".awg", FileMode.Truncate, FileAccess.Write, FileShare.None ) )
                using( StreamWriter writer = new StreamWriter( stream ) )
                    writer.Write( json );
            }
        }

        public void ChangeDefenseBaseDmg( string val ) {
            float value = float.Parse( val );
            value = Mathf.Clamp( value, 0.0f, 1.0f );
            foreach( WheelPosition pos in wheel.Selected )
                DefenseStats[pos].BaseDmgReduction = value;
        }
    }
}
