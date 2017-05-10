using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwesomeGame.EventMgmt;
using AwesomeGame.GuiMgmt;
using AwesomeGame.UtilityMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public sealed class PlayerAttack : MonoBehaviour
    {
        [SerializeField]
        GameObject wheelObj = null;

        [SerializeField]
        float maxAttackRange = 2.0f;

        [SerializeField]
        float attackTime = 3.0f;

        [SerializeField]
        float timeBetweenAttacks = 1.0f;

        [SerializeField]
        float timeToReachSlowMotion = 1.0f;

        [SerializeField]
        float slowMotionTimeScale = 0.1f;

        bool isAttacking = false;

        void Start( ) {
            InputTrigger.Instance.MouseLeftButton += InitAttack;
        }

        void InitAttack( ) {
            if( isAttacking )
                return;

            RaycastHit hit;
            if( !Physics.Raycast( transform.position, transform.forward, out hit, maxAttackRange, 1 << Constants.EnemyLayer ) )
                return;

            isAttacking = true;
            wheelObj.SetActive( true );
            CursorLock.Instance.UnlockCursor( );
            StartCoroutine( HandleSlowMotion( ) );
            StartCoroutine( HandleCooldown( ) );
            //attack logic here
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

        IEnumerator HandleCooldown( ) {
            yield return new WaitForSecondsRealtime( attackTime );
            wheelObj.SetActive( false );
            CursorLock.Instance.LockCursor( );

            yield return new WaitForSecondsRealtime( timeBetweenAttacks );
            isAttacking = false;
        }
    }
}
