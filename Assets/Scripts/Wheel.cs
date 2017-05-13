using System.Collections;
using System.Collections.Generic;
using AwesomeGame.PlayerMgmt;
using UnityEngine;

namespace AwesomeGame.WheelMgmt
{
    public class Wheel : MonoBehaviour
    {
        public static Wheel Instance { get; private set; }
        public bool IsBlocked { get; private set; }
        public bool IsClicked { get; private set; }
        public IKControl IkControl;
        public MoveObject PlayerMove;

        [SerializeField]
        GameObject wheelObj;

        [SerializeField]
        int maxAllowedMoves = 3;

        [SerializeField]
        float fromAndToNeutralStateTime = 0.4f;

        [SerializeField]
        float betweenAttackStatesTime = 0.1f;

        Dictionary<WheelPosition, WheelFragment> fragments = new Dictionary<WheelPosition, WheelFragment>( );
        Dictionary<WheelPosition, AttackPosition> attackPositions = new Dictionary<WheelPosition, AttackPosition>( );
        List<WheelPosition> selected = new List<WheelPosition>( );

        void Start( ) {
            if( Instance != null )
                Destroy( this );
            else
                Instance = this;

            IkControl.MoveTowardsObj = PlayerMove.transform;
        }

        public void AddAttackPosition( WheelPosition pos, AttackPosition att ) {
            attackPositions.Add( pos, att );
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
            StartCoroutine( Attack( isEarlyFinalize ) );
        }

        IEnumerator Attack( bool isEarlyFinalize ) {
            if( !isEarlyFinalize ) {
                foreach( WheelPosition w in selected )
                    Debug.Log( "Chosen: " + w.ToString( ) );

                PlayerMove.MoveTowards( attackPositions[selected[0]].transform, fromAndToNeutralStateTime / Time.timeScale );
                yield return new WaitUntil( ( ) => PlayerMove.HasFinishedMove );

                for( int i = 1; i < selected.Count; i++ ) {
                    PlayerMove.MoveTowards( attackPositions[selected[i]].transform, betweenAttackStatesTime / Time.timeScale );
                    yield return new WaitUntil( ( ) => PlayerMove.HasFinishedMove );
                }

                PlayerMove.MoveTowards( attackPositions[WheelPosition.Neutral].transform, fromAndToNeutralStateTime / Time.timeScale );
                yield return new WaitUntil( ( ) => PlayerMove.HasFinishedMove );

                foreach( WheelPosition w in selected )
                    fragments[w].ResetFragment( );

                selected.Clear( );
                IsClicked = false;
                IsBlocked = false;
            } else {
                IsBlocked = true;
            }

            yield return null;
        }
    }
}

