using System;
using System.Collections;
using System.Collections.Generic;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class EnemyBehaviour : EntityBehaviour
    {
        protected override void Start( ) {
            base.Start( );
            StartCoroutine( RandomTarget( ) ); //TO BE CHANGED
        }

        protected override void InitAttack( ) {
            throw new NotImplementedException( );
        }

        protected override void InitDefense( ) {
            throw new NotImplementedException( );
        }

        IEnumerator RandomTarget( ) {
            while( true ) {
                movement.MoveTowards( attackPos[(WheelPosition)UnityEngine.Random.Range( 1, 17 )].transform, betweenAttackStatesTime );
                yield return new WaitForSeconds( betweenAttackStatesTime );
            }
        }
    }
}
