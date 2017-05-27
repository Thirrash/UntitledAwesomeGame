using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using AwesomeGame.WheelMgmt;
using UnityEngine;
using AwesomeGame.UtilityMgmt;

namespace AwesomeGame.PlayerMgmt
{
    public abstract class EntityStatistic : MonoBehaviour
    {
        public event Constants.BasicEventType OnTargetChange = delegate { };

        public GameObject CurrentTarget {
            get { return currentTarget; }
            set {
                currentTarget = value;
                OnTargetChange.Invoke( );
            }
        }

        public Dictionary<WheelPosition, AttackStatistic> AttackStats { get; protected set; }
        public Dictionary<WheelPosition, DefenseStatistic> DefenseStats { get; protected set; }
        public ComboHandler ComboHandler { get; set; }
        public StaminaHandler StaminaHandler { get; set; }
        public HealthHandler HealthHandler { get; set; }

        protected GameObject currentTarget;

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

            float staminaToSpend = Mathf.Min( selected.Count * StaminaHandler.StaminaUsedPerFieldInAttack, StaminaHandler.CurrentStamina );
            float attackStaminaModifier = Mathf.Clamp( staminaToSpend / ( selected.Count * StaminaHandler.StaminaUsedPerFieldInAttack ),
                Constants.GlobalMinimumStaminaAttackModifier, 1.0f );
            StaminaHandler.CurrentStamina -= staminaToSpend;
            entity.GetComponent<EntityBehaviour>( ).InitDefense( selected, attackStat, ComboHandler, attackStaminaModifier );
        }

        public virtual void TakeDamage( List<WheelPosition> attackPosition, List<AttackStatistic> attack, List<WheelPosition> defensePosition, ComboHandler comboMultiplier, float staminaAttackModifier ) {
            float staminaToSpend = Mathf.Min( defensePosition.Count * StaminaHandler.StaminaUsedPerFieldInDefense, StaminaHandler.CurrentStamina );
            float defenseStaminaModifier = ( defensePosition.Count > 0 ) ?
                Mathf.Clamp( staminaToSpend / ( defensePosition.Count * StaminaHandler.StaminaUsedPerFieldInDefense ),
                    Constants.GlobalMinimumStaminaDefenseModifier, 1.0f ) :
                Constants.GlobalMinimumStaminaDefenseModifier;
            StaminaHandler.CurrentStamina -= staminaToSpend;

            float maxNonComboDamage = 0.0f;
            float actualNonComboDamage = 0.0f;
            for( int i = 0; i < attackPosition.Count; i++ ) {
                maxNonComboDamage += attack[i].GetBoost( );
                actualNonComboDamage += Mathf.Clamp( attack[i].GetBoost( ) -
                    ( ( defensePosition.Count > i ) ?
                        DefenseStats[attackPosition[i]].GetReduction( attackPosition[i], defensePosition[i] ) :
                        DefenseStats[attackPosition[i]].GetReduction( ) ),
                    0.0f, attack[i].GetBoost( ) );

                float percentDamageDealt = actualNonComboDamage / maxNonComboDamage;
                comboMultiplier.ChangeCombo( percentDamageDealt, true );
                ComboHandler.ChangeCombo( percentDamageDealt, false );
                Debug.Log( "Damage = " + actualNonComboDamage + " / " + maxNonComboDamage );
                HealthHandler.CurrentHealth -= actualNonComboDamage * comboMultiplier.CurrentMultiplier *
                    staminaAttackModifier / defenseStaminaModifier;
            }
        }

        protected IEnumerator UpdateComboHandler( ) {
            while( true ) {
                ComboHandler.UpdateHandler( 0.2f );
                HealthHandler.UpdateHandler( 0.2f );
                StaminaHandler.UpdateHandler( 0.2f );
                yield return new WaitForSeconds( 0.2f );
            }
        }

        protected virtual void Die( ) {
            Debug.Log( gameObject.name + " has died!" );
            Destroy( gameObject );
        }
    }
}

