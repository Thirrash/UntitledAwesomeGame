﻿using System.Collections;
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
        public bool IsAttacking = false;
        public bool IsAttacked = false;

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

        protected virtual void Update( ) {

        }

        public void JumpAway( ) {
            GetComponent<Rigidbody>( ).AddRelativeForce( Vector3.up * 5.0f, ForceMode.Impulse );
            StartCoroutine( JumpForward( ) );
        }

        public abstract void InitAttack( );
        public abstract void InitDefense( List<WheelPosition> attackPosition, 
                                          List<AttackStatistic> attack, 
                                          ComboHandler comboMultiplier, 
                                          float staminaAttackModifier,
                                          EntityBehaviour attackerBehaviour );

        IEnumerator JumpForward( ) {
            yield return new WaitForSeconds( 0.1f );
            GetComponent<Rigidbody>( ).AddRelativeForce( -Vector3.forward * 50.0f, ForceMode.Impulse );
        }
    }
}

