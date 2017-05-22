using System.Collections;
using System.Collections.Generic;
using AwesomeGame.PlayerMgmt;
using UnityEngine;

namespace AwesomeGame.WheelMgmt
{
    public class Wheel : WheelBase
    {
        public PlayerBehaviour behaviour;

        protected override void Start( ) {
            base.Start( );
        }

        public override bool AddFragment( WheelPosition pos, WheelFragment fragment ) {
            if( fragments.ContainsKey( pos ) )
                return false;

            fragments.Add( pos, fragment );
            return true;
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

