using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AwesomeGame.PlayerMgmt;
using AwesomeGame.WheelMgmt;
using UnityEngine;
using UnityEngine.UI;

namespace AwesomeGame.EditorMgmt
{
    public class InputFieldChanger : MonoBehaviour
    {
        public bool IsDefense = true;
        public Text text;

        StatisticCreation statCreation;
        FieldInfo fieldInfo;

        void Start( ) {
            statCreation = StatisticCreation.Instance;
            fieldInfo = ( typeof( DefenseStatistic ) ).GetField( gameObject.name, BindingFlags.Public | BindingFlags.Instance );
            Debug.Log( fieldInfo.Name );
            ( (WheelEditor)WheelEditor.Instance ).FieldChanger.Add( this );
        }

        public void UpdateText( ) {
            WheelEditor wheel = (WheelEditor)WheelEditor.Instance;

            if( wheel.Selected.Count == 0 ) {
                text.text = " ";
                return;
            }

            Debug.Log( statCreation.DefenseStats[wheel.Selected[0]] == null );
            string proposedText = ( IsDefense ) ? (fieldInfo.GetValue( statCreation.DefenseStats[wheel.Selected[0]] )).ToString( )
                : (fieldInfo.GetValue( statCreation.AttackStats[wheel.Selected[0]] )).ToString( );
            for( int i = 1; i < wheel.Selected.Count; i++ ) {
                if( IsDefense ) {
                    if( fieldInfo.GetValue( statCreation.DefenseStats[wheel.Selected[0]] ).ToString( ) != proposedText ) {
                        proposedText = "---";
                        break;
                    }
                } else {
                    if( fieldInfo.GetValue( statCreation.AttackStats[wheel.Selected[0]] ).ToString( ) != proposedText ) {
                        proposedText = "---";
                        break;
                    }
                }
            }

            text.text = proposedText;
        }
    }
}

