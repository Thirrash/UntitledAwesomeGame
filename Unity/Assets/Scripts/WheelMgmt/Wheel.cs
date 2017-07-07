using System.Collections;
using System.Collections.Generic;
using AwesomeGame.PlayerMgmt;
using UnityEngine;
using UnityEngine.UI;

namespace AwesomeGame.WheelMgmt
{
    public class Wheel : WheelBase
    {
        public PlayerBehaviour behaviour;
        public Text CurrentActionText;
        System.IO.StreamWriter writer;

        protected override void Start( ) {
            base.Start( );

            System.IO.FileStream stream = new System.IO.FileStream( "fragmentPos.txt", System.IO.FileMode.Append );
            writer = new System.IO.StreamWriter( stream );
        }

        public override bool AddFragment( WheelPosition pos, WheelFragment fragment ) {
            if( fragments.ContainsKey( pos ) )
                return false;

            fragments.Add( pos, fragment );

            //writer.WriteLine( "{ WheelPosition." + fragment.gameObject.name +
            //    ", new Vector2( " + fragment.transform.localPosition.x + "f, " + fragment.transform.localPosition.z + "f) }," );
            return true;
        }

        void OnDestroy( ) {
            writer.Close( );
            writer.Dispose( );
        }

        public override bool ActivateFragment( WheelPosition pos ) {
            if( Selected.Count >= behaviour.MaxAllowedMoves ) {
                IsBlocked = true;
                return false;
            }

            IsClicked = true;
            Selected.Add( pos );
            return true;
        }

        public override void ToggleActivation( bool isActive ) {
            wheelObj.SetActive( isActive );
        }

        public override void Finalize( bool isEarlyFinalize ) {
            behaviour.ChangeSelected( Selected );
            Reset( isEarlyFinalize );
        }

        void Reset( bool isEarlyFinalize ) {
            if( !isEarlyFinalize ) {
                foreach( WheelPosition w in Selected )
                    fragments[w].ResetFragment( );

                Selected.Clear( );
                IsClicked = false;
                IsBlocked = false;
            } else {
                IsBlocked = true;
            }
        }
    }
}

