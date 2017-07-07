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
        public List<InputFieldChanger> FieldChanger;

        protected override void Start( ) {
            base.Start( );
            IsBlocked = true;
            FieldChanger = new List<InputFieldChanger>( );

            foreach( EnemyType type in (EnemyType[])Enum.GetValues( typeof( EnemyType ) ) ) {
                GameObject tmpObj = Instantiate<GameObject>( ButtonObj, ContentObj.transform );
                tmpObj.GetComponentInChildren<Text>( ).text = type.ToString( );
            }
        }

        public override bool AddFragment( WheelPosition pos, WheelFragment fragment ) {
            if( fragments.ContainsKey( pos ) )
                return false;

            fragments.Add( pos, fragment );
            return true;
        }

        public override bool ActivateFragment( WheelPosition pos ) {
            if( Selected.Contains( pos ) )
                return false;

            if( !Input.GetKey( KeyCode.LeftControl ) )
                Deselect( );

            IsClicked = true;
            Selected.Add( pos );
            foreach( InputFieldChanger i in FieldChanger )
                i.UpdateText( );
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
            Selected.Clear( );
        }

        public void ChangeMode( ) {
            IsDefenseMode = !IsDefenseMode;
            ModeTextObj.GetComponent<Text>( ).text = ( IsDefenseMode ) ? "Defense" : "Attack";
        }
    }
}

