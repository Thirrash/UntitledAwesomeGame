using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AwesomeGame.WheelMgmt
{
    public class WheelFragment : MonoBehaviour
    {
        public WheelPosition WheelPos { get; private set; }

        bool isChosen = false;
        WheelBase wheel;
        Material currMat;
        Dictionary<string, Color> matColor = new Dictionary<string, Color> {
            { "Neutral", new Color( 1.0f, 1.0f, 1.0f, 0.2f ) },
            { "Highlighted", new Color( 1.0f, 0.0f, 0.0f, 0.5f ) },
            { "Selected", new Color( 1.0f, 0.0f, 0.0f, 0.8f ) }
        };

        void Start( ) {
            WheelPos = (WheelPosition)Enum.Parse( typeof( WheelPosition ), gameObject.name );
            wheel = WheelBase.Instance;
            wheel.AddFragment( WheelPos, this );
            currMat = GetComponent<MeshRenderer>( ).material;
            currMat.color = matColor["Neutral"];
        }

        void Update( ) {

        }

        void OnMouseEnter( ) {
            if( wheel.IsBlocked )
                return;

            if( !wheel.IsClicked ) {
                currMat.color = matColor["Highlighted"];
            } else {
                if( wheel.ActivateFragment( WheelPos ) ) {
                    isChosen = true;
                    currMat.color = matColor["Selected"];
                }
            }
        }

        void OnMouseExit( ) {
            if( wheel.IsBlocked )
                return;

            if( !wheel.IsClicked )
                currMat.color = matColor["Neutral"];
        }

        void OnMouseDown( ) {
            if( wheel.ActivateFragment( WheelPos ) ) {
                isChosen = true;
                currMat.color = matColor["Selected"];
            }
        }

        void OnMouseUp( ) {
            wheel.Finalize( true );
        }

        public void ResetFragment( ) {
            isChosen = false;
            currMat.color = matColor["Neutral"];
        }
    }
}
