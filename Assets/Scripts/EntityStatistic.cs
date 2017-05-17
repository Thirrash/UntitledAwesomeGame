using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public abstract class EntityStatistic<T> : MonoBehaviour where T : EntityStatistic<T>
    {
        public Dictionary<WheelPosition, AttackStatistic> AttackStats { get; protected set; }
        public Dictionary<WheelPosition, DefenseStatistic> DefenseStats { get; protected set; }

        protected static Dictionary<WheelPosition, AttackStatistic> attackStatsBase;
        protected static Dictionary<WheelPosition, DefenseStatistic> defenseStatsBase;

        static EntityStatistic( ) {
            attackStatsBase = new Dictionary<WheelPosition, AttackStatistic>( );
            defenseStatsBase = new Dictionary<WheelPosition, DefenseStatistic>( );
        }

        protected virtual void Start( ) {
            //AttackStats = attackStatsBase.CloneDictionaryCloningValues( );
            //Debug.Log( AttackStats.Count );
        }

        protected static void InitBaseStats( string path ) {
            foreach( WheelPosition pos in (WheelPosition[])Enum.GetValues( typeof( WheelPosition ) ) ) {
                if( pos == WheelPosition.Neutral )
                    continue;

                string json = "";
                using( FileStream stream = new FileStream( path + "_attack_" + pos.ToString( ) + ".awg", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read ) )
                using( StreamReader reader = new StreamReader( stream ) )
                    json = reader.ReadToEnd( );
                attackStatsBase.Add( pos, JsonUtility.FromJson<AttackStatistic>( json ) );

                using( FileStream stream = new FileStream( path + "_defense_" + pos.ToString( ) + ".awg", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read ) )
                using( StreamReader reader = new StreamReader( stream ) )
                    json = reader.ReadToEnd( );
                defenseStatsBase.Add( pos, JsonUtility.FromJson<DefenseStatistic>( json ) );
            }
        }
    }
}

