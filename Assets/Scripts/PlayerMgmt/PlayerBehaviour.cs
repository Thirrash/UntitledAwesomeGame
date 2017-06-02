using System;
using System.Collections;
using System.Collections.Generic;
using AwesomeGame.EventMgmt;
using AwesomeGame.GuiMgmt;
using AwesomeGame.UtilityMgmt;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class PlayerBehaviour : EntityBehaviour
    {
        [SerializeField]
        float attackTime = 3.0f;

        [SerializeField]
        float defenseTime = 3.0f;

        [SerializeField]
        float timeToReachSlowMotion = 1.0f;

        [SerializeField]
        float slowMotionTimeScale = 0.1f;

        Wheel wheel;
        PlayerStatistic stat;

        protected override void Start( ) {
            base.Start( );
            InputTrigger.Instance.MouseLeftButton += InitAttack;
            stat = GetComponent<PlayerStatistic>( );
            wheel = GetComponent<Wheel>( );
        }

        protected override void Update( ) {
            RaycastHit hit;
            if( !Physics.Raycast( transform.position, transform.forward, out hit, 25.0f, 1 << Constants.EnemyLayer ) ) {
                if( stat.CurrentTarget != null )
                    stat.CurrentTarget = null;
                return;
            }

            stat.CurrentTarget = hit.collider.gameObject;
        }

        public override void InitAttack( ) {
            if( IsAttacking || IsAttacked )
                return;

            if( stat.CurrentTarget == null )
                return;

            if( Vector2.Distance( transform.position, stat.CurrentTarget.transform.position ) > maxAttackRange )
                return;

            if( stat.CurrentTarget.GetComponent<EntityBehaviour>( ).IsAttacked )
                return;

            GameObject lastTarget = stat.CurrentTarget;
            IsAttacking = true;
            wheel.ToggleActivation( true );
            wheel.CurrentActionText.text = "Attack mode";
            CursorLock.Instance.UnlockCursor( );
            StartCoroutine( HandleSlowMotionAttack( ) );
            StartCoroutine( HandleCooldownAttack( ( ) => lastTarget ) );
        }

        public override void InitDefense( List<WheelPosition> attackPosition, 
                                          List<AttackStatistic> attack, 
                                          ComboHandler comboMultiplier,
                                          float staminaAttackModifier,
                                          EntityBehaviour attackerBehaviour) {
            IsAttacked = true;
            
            StartCoroutine( HandleSlowMotionDefense( ) );
            StartCoroutine( HandleCooldownDefense( attackPosition, attack, comboMultiplier, staminaAttackModifier, attackerBehaviour ) );
        }

        IEnumerator HandleSlowMotionAttack( ) {
            for( float i = 1.0f; i > 0.01f; i -= 0.1f ) {
                Time.timeScale = i;
                yield return new WaitForSecondsRealtime( timeToReachSlowMotion / 10.0f );
            }

            yield return new WaitForSecondsRealtime( attackTime - 2.0f * timeToReachSlowMotion );

            for( float i = 0.1f; i < 1.01f; i += 0.1f ) {
                Time.timeScale = i;
                yield return new WaitForSecondsRealtime( timeToReachSlowMotion / 10.0f );
            }
        }

        IEnumerator HandleCooldownAttack( Func<GameObject> lastTargetReference ) {
            yield return new WaitForSecondsRealtime( attackTime );

            wheel.Finalize( false );
            StartCoroutine( Attack( ) );
            CursorLock.Instance.LockCursor( );
            wheel.ToggleActivation( false );

            stat.DealDamage( lastTargetReference( ), selected );

            yield return new WaitForSecondsRealtime( timeBetweenAttacks );
            IsAttacking = false;
        }

        IEnumerator HandleSlowMotionDefense( ) {
            for( float i = 1.0f; i > 0.01f; i -= 0.1f ) {
                Time.timeScale = i;
                yield return new WaitForSecondsRealtime( timeToReachSlowMotion / 10.0f );
            }

            wheel.ToggleActivation( true );
            wheel.CurrentActionText.text = "Defense mode";
            CursorLock.Instance.UnlockCursor( );
            yield return new WaitForSecondsRealtime( defenseTime - 2.0f * timeToReachSlowMotion );

            for( float i = 0.1f; i < 1.01f; i += 0.1f ) {
                Time.timeScale = i;
                yield return new WaitForSecondsRealtime( timeToReachSlowMotion / 10.0f );
            }
        }

        IEnumerator HandleCooldownDefense( List<WheelPosition> attackPosition, 
                                           List<AttackStatistic> attack, 
                                           ComboHandler comboMultiplier, 
                                           float staminaAttackModifier,
                                           EntityBehaviour attackerBehaviour ) {
            yield return new WaitForSecondsRealtime( defenseTime );

            wheel.Finalize( false );
            StartCoroutine( Attack( ) );
            CursorLock.Instance.LockCursor( );
            wheel.ToggleActivation( false );

            stat.TakeDamage( attackPosition, attack, selected, comboMultiplier, staminaAttackModifier );
            JumpAway( );
            attackerBehaviour.JumpAway( );

            yield return new WaitForSeconds( timeBetweenAttacks );
            IsAttacked = false;
        }

        IEnumerator Attack( ) {
            if( selected.Count == 0 )
                yield break;

            movement.MoveTowards( attackPos[selected[0]].transform, fromAndToNeutralStateTime / Time.timeScale );
            yield return new WaitUntil( ( ) => movement.HasFinishedMove );

            for( int i = 1; i < selected.Count; i++ ) {
                movement.MoveTowards( attackPos[selected[i]].transform, betweenAttackStatesTime / Time.timeScale );
                yield return new WaitUntil( ( ) => movement.HasFinishedMove );
            }

            movement.MoveTowards( attackPos[WheelPosition.Neutral].transform, fromAndToNeutralStateTime / Time.timeScale );
            yield return new WaitUntil( ( ) => movement.HasFinishedMove );
        }
    }
}

