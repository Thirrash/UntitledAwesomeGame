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
        float timeToReachSlowMotion = 1.0f;

        [SerializeField]
        float slowMotionTimeScale = 0.1f;

        Wheel wheel;
        PlayerStatistic stat;

        protected override void Start( ) {
            base.Start( );
            InputTrigger.Instance.MouseLeftButton += InitAttack;
            wheel = (Wheel)Wheel.Instance;
            stat = GetComponent<PlayerStatistic>( );
        }

        void Update( ) {
            RaycastHit hit;
            if( !Physics.Raycast( transform.position, transform.forward, out hit, 25.0f, 1 << Constants.EnemyLayer ) ) {
                if( stat.CurrentTarget != null )
                    stat.CurrentTarget = null;
                return;
            }

            stat.CurrentTarget = hit.collider.gameObject.GetComponent<EntityStatistic>( );
        }

        protected override void InitAttack( ) {
            if( isAttacking )
                return;

            if( stat.CurrentTarget == null )
                return;

            if( Vector2.Distance( transform.position, stat.CurrentTarget.transform.position ) > maxAttackRange )
                return;

            EntityStatistic lastTarget = stat.CurrentTarget;
            isAttacking = true;
            wheel.ToggleActivation( true );
            CursorLock.Instance.UnlockCursor( );
            StartCoroutine( HandleSlowMotion( ) );
            StartCoroutine( HandleCooldown( ( ) => lastTarget.gameObject, ( ) => selected ) );
        }

        protected override void InitDefense( ) {
            throw new NotImplementedException( );
        }

        IEnumerator HandleSlowMotion( ) {
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

        IEnumerator HandleCooldown( Func<GameObject> hitObjReference, Func<List<WheelPosition>> selectedListReference ) {
            yield return new WaitForSecondsRealtime( attackTime );

            wheel.Finalize( false );
            StartCoroutine( Attack( ) );
            CursorLock.Instance.LockCursor( );
            wheel.ToggleActivation( false );

            stat.DealDamage( hitObjReference( ), selectedListReference( ) );

            yield return new WaitForSecondsRealtime( timeBetweenAttacks );
            isAttacking = false;
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

