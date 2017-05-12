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
        Wheel wheel;
        Sprite[] wheelSprites = new Sprite[3];
        Image wheelImage;

        void Start( ) {
            WheelPos = (WheelPosition)Enum.Parse( typeof( WheelPosition ), gameObject.name );
            wheel = Wheel.Instance;
            wheel.AddFragment( WheelPos, this );

            wheelImage = GetComponentInChildren<Image>( );
            for( int i = 0; i < 3; i++ )
                wheelSprites[i] = Resources.Load<Sprite>( "Textures/Wheel/" + gameObject.name + "_" + i );
            wheelImage.overrideSprite = wheelSprites[0];
        }

        void Update( ) {

        }

        void OnMouseEnter( ) {
            if( !wheel.IsClicked ) {
                wheelImage.overrideSprite = wheelSprites[1];
            } else {
                if( wheel.ActivateFragment( WheelPos ) ) {
                    isChosen = true;
                    wheelImage.overrideSprite = wheelSprites[2];
                }
            }
        }

        void OnMouseExit( ) {
            if( !wheel.IsClicked )
                wheelImage.overrideSprite = wheelSprites[0];
        }

        void OnMouseDown( ) {
            if( wheel.ActivateFragment( WheelPos ) ) {
                isChosen = true;
                wheelImage.overrideSprite = wheelSprites[2];
            }
        }

        void OnMouseUp( ) {
            wheel.Finalize( true );
        }

        public void ResetFragment( ) {
            isChosen = false;
            wheelImage.overrideSprite = wheelSprites[0];
        }
    }
}
