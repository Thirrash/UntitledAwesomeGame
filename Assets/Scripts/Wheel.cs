using System.Collections;
using System.Collections.Generic;
using AwesomeGame.PlayerMgmt;
using UnityEngine;

namespace AwesomeGame.WheelMgmt
{
    public class Wheel : WheelBase
    {
        public IKControl IkControl;
        public MoveObject PlayerMove;

        [SerializeField]
        int maxAllowedMoves = 3;

        [SerializeField]
        float fromAndToNeutralStateTime = 0.4f;

        [SerializeField]
        float betweenAttackStatesTime = 0.1f;

        Dictionary<WheelPosition, AttackPosition> attackPositions = new Dictionary<WheelPosition, AttackPosition>( );

        protected override void Start( ) {
            base.Start( );

            if( IkControl != null )
                IkControl.MoveTowardsObj = PlayerMove.transform;
        }

        public void AddAttackPosition( WheelPosition pos, AttackPosition att ) {
            attackPositions.Add( pos, att );
        }

        public override bool AddFragment( WheelPosition pos, WheelFragment fragment ) {
            if( fragments.ContainsKey( pos ) )
                return false;

            fragments.Add( pos, fragment );
            return true;
        }

        public override bool ActivateFragment( WheelPosition pos ) {
            if( Selected.Count >= maxAllowedMoves ) {
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
            StartCoroutine( Attack( isEarlyFinalize ) );
        }

        IEnumerator Attack( bool isEarlyFinalize ) {
            if( !isEarlyFinalize ) {
                foreach( WheelPosition w in Selected )
                    Debug.Log( "Chosen: " + w.ToString( ) );

                PlayerMove.MoveTowards( attackPositions[Selected[0]].transform, fromAndToNeutralStateTime / Time.timeScale );
                yield return new WaitUntil( ( ) => PlayerMove.HasFinishedMove );

                for( int i = 1; i < Selected.Count; i++ ) {
                    PlayerMove.MoveTowards( attackPositions[Selected[i]].transform, betweenAttackStatesTime / Time.timeScale );
                    yield return new WaitUntil( ( ) => PlayerMove.HasFinishedMove );
                }

                PlayerMove.MoveTowards( attackPositions[WheelPosition.Neutral].transform, fromAndToNeutralStateTime / Time.timeScale );
                yield return new WaitUntil( ( ) => PlayerMove.HasFinishedMove );

                foreach( WheelPosition w in Selected )
                    fragments[w].ResetFragment( );

                Selected.Clear( );
                IsClicked = false;
                IsBlocked = false;
            } else {
                IsBlocked = true;
            }

            yield return null;
        }
    }
}

