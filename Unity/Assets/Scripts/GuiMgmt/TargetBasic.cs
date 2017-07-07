using System;
using System.Collections;
using System.Collections.Generic;
using AwesomeGame.PlayerMgmt;
using UnityEngine;
using UnityEngine.UI;

namespace AwesomeGame.GuiMgmt
{
    public class TargetBasic : PlayerBasic
    {
        protected override void Start( ) {
            StartCoroutine( UpdateTargetStats( ) );
        }

        public void ChangeTarget( GameObject newTarget ) {
            stats = newTarget.GetComponent<EntityStatistic>( );
            OnHealthChange( );
            OnStaminaChange( );
            OnComboChange( );
        }

        public void ChangeTargetToNull( ) {
            stats = null;
        }

        IEnumerator UpdateTargetStats( ) {
            while( true ) {
                yield return new WaitForSeconds( 0.1f );
                if( stats == null )
                    continue;

                OnHealthChange( );
                OnStaminaChange( );
                OnComboChange( );
            }
        }
    }
}

