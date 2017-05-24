using System.Collections;
using System.Collections.Generic;
using AwesomeGame.WheelMgmt;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public abstract class EntityBehaviour : MonoBehaviour
    {
        public int MaxAllowedMoves = 3;

        protected Dictionary<WheelPosition, EntityAttackPosition> attackPos = new Dictionary<WheelPosition, EntityAttackPosition>( );
        protected List<WheelPosition> selected = new List<WheelPosition>( );
        protected IKControl ikControl;
        protected MoveObject movement;
        protected bool isAttacking;

        [SerializeField]
        protected float fromAndToNeutralStateTime = 0.4f;

        [SerializeField]
        protected float betweenAttackStatesTime = 0.1f;

        [SerializeField]
        protected float maxAttackRange = 2.0f;

        [SerializeField]
        protected float timeBetweenAttacks = 1.0f;

        protected virtual void Start( ) {
            ikControl = GetComponent<IKControl>( );
            movement = GetComponentInChildren<MoveObject>( );
            if( ikControl != null )
                ikControl.MoveTowardsObj = movement.transform;
        }

        public void AddPosition( WheelPosition fragment, EntityAttackPosition pos ) {
            attackPos.Add( fragment, pos );
        }

        public void ChangeSelected( List<WheelPosition> newSelected ) {
            selected = new List<WheelPosition>( newSelected );
        }

        protected abstract void InitAttack( );
        protected abstract void InitDefense( );
    }
}

