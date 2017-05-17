using System;
using System.Collections;
using System.Collections.Generic;
using AwesomeGame.WheelMgmt;
using UnityEngine;
using UnityEngine.UI;

namespace AwesomeGame.EditorMgmt
{
    public class WheelEditor : WheelBase
    {
        public bool IsDefenseMode = true;
        public GameObject ModeTextObj;
        public GameObject ContentObj;
        public GameObject ButtonObj;

        protected override void Start( ) {
            base.Start( );
            IsBlocked = true;

            foreach( EnemyType type in (EnemyType[])Enum.GetValues( typeof( EnemyType ) ) ) {
                GameObject tmpObj = Instantiate<GameObject>( ButtonObj, ContentObj.transform );
            }
        }

        public override bool AddFragment( WheelPosition pos, WheelFragment fragment ) {
            if( fragments.ContainsKey( pos ) )
                return false;

            fragments.Add( pos, fragment );
            return true;
        }

        public override bool ActivateFragment( WheelPosition pos ) {
            if( selected.Contains( pos ) )
                return false;

            if( !Input.GetKey( KeyCode.LeftControl ) )
                Deselect( ); Debug.Log( "g" );

            IsClicked = true;
            selected.Add( pos );
            return true;
        }

        public override void ToggleActivation( bool isActive ) {
            wheelObj.SetActive( isActive );
        }

        public override void Finalize( bool isEarlyFinalize ) {

        }

        public void Deselect( ) {
            foreach( WheelFragment f in fragments.Values )
                f.ResetFragment( );

            IsClicked = false;
            selected.Clear( );
        }

        public void ChangeMode( ) {
            IsDefenseMode = !IsDefenseMode;
            ModeTextObj.GetComponent<Text>( ).text = ( IsDefenseMode ) ? "Defense" : "Attack";
        }
    }
}

