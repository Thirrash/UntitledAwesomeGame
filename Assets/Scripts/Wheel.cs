using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeGame.WheelMgmt
{
    public class Wheel : MonoBehaviour
    {
        public static Wheel Instance { get; private set; }
        public bool IsBlocked { get; private set; }
        public bool IsClicked { get; private set; }

        [SerializeField]
        GameObject wheelObj;

        [SerializeField]
        int maxAllowedMoves = 3;

        Dictionary<WheelPosition, WheelFragment> fragments = new Dictionary<WheelPosition, WheelFragment>( );
        List<WheelPosition> selected = new List<WheelPosition>( );

        void Start( ) {
            if( Instance != null )
                Destroy( this );
            else
                Instance = this;
        }

        void Update( ) {

        }

        public void AddFragment( WheelPosition pos, WheelFragment fragment ) {
            fragments.Add( pos, fragment );
        }

        public bool ActivateFragment( WheelPosition pos ) {
            if( selected.Count >= maxAllowedMoves ) {
                IsBlocked = true;
                return false;
            }

            if( IsBlocked )
                return false;

            IsClicked = true;
            selected.Add( pos );
            return true;
        }

        public void ToggleActivation( bool isActive ) {
            wheelObj.SetActive( isActive );
        }

        public void Finalize( bool isEarlyFinalize ) {
            //wheel logic

            if( !isEarlyFinalize ) {
                foreach( WheelPosition w in selected )
                    Debug.Log( "Chosen: " + w.ToString( ) );

                foreach( KeyValuePair<WheelPosition, WheelFragment> k in fragments )
                    k.Value.ResetFragment( );
                selected.Clear( );
                IsClicked = false;
                IsBlocked = false;
            } else {
                IsBlocked = true;
            }
        }
    }
}

