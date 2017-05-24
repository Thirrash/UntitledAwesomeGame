using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace AwesomeGame.EditorMgmt
{
    public class EnemyTypeChooser : MonoBehaviour
    {
        EnemyType type;

        void Start( ) {
            type = (EnemyType)Enum.Parse( typeof( EnemyType ), GetComponentInChildren<Text>( ).text );
        }

        public void ChangeEnemyType( ) {
            StatisticCreation.Instance.ChangeEnemyType( type );
        }
    }
}
