using System;
using System.Collections;
using System.Collections.Generic;
using AwesomeGame.UtilityMgmt;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class EnemyTestBehaviour : EntityBehaviour
    {
        EnemyTestStatistic stat;

        protected override void Start( ) {
            base.Start( );
            stat = GetComponent<EnemyTestStatistic>( );
            StartCoroutine( RandomTarget( ) );
        }

        public override void InitAttack( ) {
            if( IsAttacking || IsAttacked )
                return;

            if( stat.CurrentTarget == null )
                return;

            if( stat.CurrentTarget.GetComponent<EntityBehaviour>( ).IsAttacked )
                return;

            List<WheelPosition> rolledPos = new List<WheelPosition> {
                WheelPosition.OuterRight, WheelPosition.OuterLeft
            };

            StartCoroutine( AttackAnim( ( ) => rolledPos ) );
            stat.DealDamage( stat.CurrentTarget, rolledPos );
        }

        public override void InitDefense( List<WheelPosition> attackPosition, List<AttackStatistic> attack, ComboHandler comboMultiplier, float staminaAttackModifier ) {
            List<WheelPosition> rolledPos = new List<WheelPosition> {
                WheelPosition.OuterTop, WheelPosition.OuterBottom
            };

            StartCoroutine( AttackAnim( ( ) => rolledPos ) );
            stat.TakeDamage( attackPosition, attack, rolledPos, comboMultiplier, staminaAttackModifier );
        }

        IEnumerator RandomTarget( ) {
            while( true ) {
                Collider[] playerInRangeCol = Physics.OverlapSphere( transform.position, maxAttackRange, 1 << Constants.PlayerLayer );
                if( playerInRangeCol.Length > 0 ) {
                    stat.CurrentTarget = playerInRangeCol[0].gameObject;
                    InitAttack( );
                }

                yield return new WaitForSeconds( 5.0f );
            }
        }

        IEnumerator AttackAnim( Func<List<WheelPosition>> selected ) {
            if( selected( ).Count == 0 )
                yield break;

            movement.MoveTowards( attackPos[selected( )[0]].transform, fromAndToNeutralStateTime / Time.timeScale );
            yield return new WaitUntil( ( ) => movement.HasFinishedMove );

            for( int i = 1; i < selected( ).Count; i++ ) {
                movement.MoveTowards( attackPos[selected( )[i]].transform, betweenAttackStatesTime / Time.timeScale );
                yield return new WaitUntil( ( ) => movement.HasFinishedMove );
            }

            movement.MoveTowards( attackPos[WheelPosition.Neutral].transform, fromAndToNeutralStateTime / Time.timeScale );
            yield return new WaitUntil( ( ) => movement.HasFinishedMove );
        }
    }
}
