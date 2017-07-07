using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace AwesomeGame.SoundMgmt
{
    public class SoundHandler : MonoBehaviour
    {
        public Dictionary<SoundType, AudioClip> Sounds { get; private set; }

        [SerializeField]
        List<SoundType> soundTypes = new List<SoundType>( );

        [SerializeField]
        List<AudioClip> soundClips = new List<AudioClip>( );

        AudioSource audioSource;

        void Start( ) {
            audioSource = GetComponent<AudioSource>( );
            Sounds = new Dictionary<SoundType, AudioClip>( );
            if( soundTypes.Count != soundClips.Count )
                Debug.LogError( "SoundTypes count does not equal SoundClips count in gameObject = " + gameObject.name );

            for( int i = 0; i < soundTypes.Count; i++ )
                Sounds.Add( soundTypes[i], soundClips[i] );
        }

        public bool PlayClip( AnimationEvent e ) {
            if( !Sounds.ContainsKey( (SoundType)Enum.Parse( typeof( SoundType ), e.stringParameter ) ) )
                return false;

            audioSource.PlayOneShot( Sounds[(SoundType)Enum.Parse( typeof( SoundType ), e.stringParameter )] );
            return true;
        }

        public bool PlayClip( SoundType type ) {
            if( !Sounds.ContainsKey( type ) )
                return false;

            audioSource.PlayOneShot( Sounds[type] );
            return true;
        }
    }
}

