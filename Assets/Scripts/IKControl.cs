using UnityEngine;
using System;
using System.Collections;
using AwesomeGame.WheelMgmt;

public class IKControl : MonoBehaviour
{
    public bool IsActive = false;
    public Transform MoveTowardsObj = null;
    public Transform LookAtObj = null;

    Animator animator;

    void Start( ) {
        animator = GetComponent<Animator>( );
    }

    void OnAnimatorIK( ) {
        if( !animator )
            return;

        if( IsActive ) {
            if( LookAtObj != null ) {
                animator.SetLookAtWeight( 1 );
                animator.SetLookAtPosition( LookAtObj.position );
            }

            if( MoveTowardsObj != null ) {
                animator.SetIKPositionWeight( AvatarIKGoal.RightHand, 1 );
                animator.SetIKRotationWeight( AvatarIKGoal.RightHand, 1 );
                animator.SetIKPosition( AvatarIKGoal.RightHand, MoveTowardsObj.position );
                animator.SetIKRotation( AvatarIKGoal.RightHand, MoveTowardsObj.rotation );
            }
        } else {
            animator.SetIKPositionWeight( AvatarIKGoal.RightHand, 0 );
            animator.SetIKRotationWeight( AvatarIKGoal.RightHand, 0 );
            animator.SetLookAtWeight( 0 );
        }
    }
}