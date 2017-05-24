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
        [SerializeField]
        float maxHitPoints;
        public float MaxHitPoints {
            get { return maxHitPoints; }
            protected set { maxHitPoints = value; }
        }

        float hitPoints;
        public float HitPoints {
            get { return hitPoints; }
            protected set {
                hitPoints = value;
                if( hitPoints <= 0.0f ) {
                    hitPoints = 0.0f;
                    Die( );
                }
            }
        }

        public Dictionary<WheelPosition, AttackStatistic> AttackStats { get; protected set; }
        public Dictionary<WheelPosition, DefenseStatistic> DefenseStats { get; protected set; }

        ComboHandler comboHandler;

        protected virtual void Start( ) {
            comboHandler = new ComboHandler( );
            StartCoroutine( UpdateComboHandler( ) );
        }

        protected static void InitBaseStats( Dictionary<WheelPosition, AttackStatistic> baseAttack,
                                             Dictionary<WheelPosition, DefenseStatistic> baseDefense, 
                                             string path ) {
            foreach( WheelPosition pos in (WheelPosition[])Enum.GetValues( typeof( WheelPosition ) ) ) {
                if( pos == WheelPosition.Neutral )
                    continue;

                string json = "";
                using( FileStream stream = new FileStream( path + "_attack_" + pos.ToString( ) + ".awg", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read ) )
                using( StreamReader reader = new StreamReader( stream ) )
                    json = reader.ReadToEnd( );
                baseAttack.Add( pos, JsonUtility.FromJson<AttackStatistic>( json ) );

                using( FileStream stream = new FileStream( path + "_defense_" + pos.ToString( ) + ".awg", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read ) )
                using( StreamReader reader = new StreamReader( stream ) )
                    json = reader.ReadToEnd( );
                baseDefense.Add( pos, JsonUtility.FromJson<DefenseStatistic>( json ) );
            }
        }

        protected virtual void Die( ) {
            Debug.Log( gameObject.name + " has died!" );
            Destroy( gameObject );
        }

        public void DealDamage( GameObject entity, List<WheelPosition> selected ) {
            Dictionary<WheelPosition, AttackStatistic> selectedAttack = AttackStats.CloneCertainValues( selected );
            entity.GetComponent<EntityStatistic>( ).TakeDamage( selectedAttack, comboHandler );
        }

        public virtual void TakeDamage( Dictionary<WheelPosition, AttackStatistic> attackDictionary, ComboHandler comboMultiplier ) {

        }

        protected IEnumerator UpdateComboHandler( ) {
            while( true ) {
                comboHandler.UpdateCombo( 0.1f );
                yield return new WaitForSeconds( 0.1f );
            }
        }
    }
}

