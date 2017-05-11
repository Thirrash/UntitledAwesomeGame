using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AwesomeGame.PlayerMgmt
{
    public class MoveObject : MonoBehaviour
    {
        float timer = 0.0f;
        public float TimeToMove = 1.0f;
        Vector3 startLocalPos;
        public Vector3 endLocalPos;

        void Start( ) {
            startLocalPos = transform.localPosition;
            StartCoroutine( Move( ) );
        }

        void Update( ) {
            timer += Time.deltaTime;
        }

        IEnumerator Move( ) {
            while( true ) {
                if( timer >= TimeToMove ) {
                    Vector3 tmp = endLocalPos;
                    endLocalPos = startLocalPos;
                    startLocalPos = tmp;
                    timer = 0.0f;
                }

                transform.localPosition = Vector3.Lerp(startLocalPos, endLocalPos, timer / TimeToMove);
                yield return null;
            }
        }
    }

}
