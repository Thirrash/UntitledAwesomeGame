using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public abstract class EntityStatistic : MonoBehaviour
    {
        public Dictionary<WheelPosition, AttackStatistic> AttackStats { get; protected set; }
        public Dictionary<WheelPosition, DefenseStatistic> DefenseStats { get; protected set; }

        public ComboHandler ComboHandler { get; set; }
        public StaminaHandler StaminaHandler { get; set; }
        public HealthHandler HealthHandler { get; set; }

        protected virtual void Start( ) {
            ComboHandler = new ComboHandler( );
            StaminaHandler = new StaminaHandler( );
            HealthHandler = new HealthHandler( );
            HealthHandler.OnDie += Die;
            StartCoroutine( UpdateComboHandler( ) );
        }

        protected static void InitBaseStats( Dictionary<WheelPosition, AttackStatistic> baseAttack,
                                             Dictionary<WheelPosition, DefenseStatistic> baseDefense,
                                             string path ) {
            foreach( WheelPosition pos in (WheelPosition[])Enum.GetValues( typeof( WheelPosition ) ) ) {
                if( pos == WheelPosition.Neutral )
                    continue;

                Debug.Log( pos.ToString( ) + baseDefense.Count );
                string json = "";
                using( FileStream stream = new FileStream( path + "_attack_" + pos.ToString( ) + ".awg", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read ) )
                using( StreamReader reader = new StreamReader( stream ) )
                    json = reader.ReadToEnd( );
                AttackStatistic attack = JsonUtility.FromJson<AttackStatistic>( json );
                if( attack != null )
                    baseAttack.Add( pos, attack );
                else
                    Debug.LogWarning( "AttackStat is null in " + path ); baseAttack.Add( pos, new AttackStatistic( ) );

                using( FileStream stream = new FileStream( path + "_defense_" + pos.ToString( ) + ".awg", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read ) )
                using( StreamReader reader = new StreamReader( stream ) )
                    json = reader.ReadToEnd( );
                DefenseStatistic def = JsonUtility.FromJson<DefenseStatistic>( json );
                if( def != null )
                    baseDefense.Add( pos, def );
                else {
                    Debug.LogWarning( "DefenseStat is null in " + path );
                    baseDefense.Add( pos, new DefenseStatistic( ) );
                }
            }
        }

        public void DealDamage( GameObject entity, List<WheelPosition> selected ) {
            List<AttackStatistic> attackStat = new List<AttackStatistic>( AttackStats.CloneCertainValues( selected ).Values );
            entity.GetComponent<EntityStatistic>( ).TakeDamage( selected, attackStat, ComboHandler );
        }

        public virtual void TakeDamage( List<WheelPosition> position, List<AttackStatistic> attack, ComboHandler comboMultiplier ) {

        }

        protected IEnumerator UpdateComboHandler( ) {
            while( true ) {
                ComboHandler.UpdateHandler( 0.2f );
                yield return new WaitForSeconds( 0.2f );
            }
        }

        protected virtual void Die( ) {
            Debug.Log( gameObject.name + " has died!" );
            Destroy( gameObject );
        }
    }
}

