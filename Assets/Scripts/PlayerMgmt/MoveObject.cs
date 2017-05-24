using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class MoveObject : MonoBehaviour
    {
        public bool HasFinishedMove { get; private set; }
        float timer = 0.0f;
        Vector3 startPos;
        Vector3 endPos;
        float timeToMove;

        Vector3 invokingPrevPos;
        Coroutine moveCoroutine;

        public void MoveTowards( Transform endPositionTrans, float moveTime, Vector3 startPosition = default(Vector3) ) {
            if( startPosition != default(Vector3) )
                startPos = startPosition;
            else
                startPos = transform.position;

            endPos = endPositionTrans.position;
            timeToMove = moveTime;
            invokingPrevPos = endPositionTrans.position;
            HasFinishedMove = false;

            if( moveCoroutine != null )
                StopCoroutine( moveCoroutine );
            moveCoroutine = StartCoroutine( Move( ( ) => endPositionTrans ) );
        }

        IEnumerator Move( Func<Transform> invokingTransformFunc ) {
            while( true ) {
                timer += Time.deltaTime;
                if( timer >= timeToMove ) {
                    timer = 0.0f;
                    startPos = endPos;
                    transform.position = endPos;
                    HasFinishedMove = true;
                    yield break;
                }

                Vector3 invokingOffset = invokingTransformFunc().position - invokingPrevPos;
                startPos += invokingOffset;
                endPos += invokingOffset;
                invokingPrevPos = invokingTransformFunc().position;

                transform.position = Vector3.Lerp(startPos, endPos, timer / timeToMove);
                yield return null;
            }
        }
    }

}
