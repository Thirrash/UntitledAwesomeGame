using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwesomeGame.PlayerMgmt;
using UnityEngine;
using UnityEngine.UI;

namespace AwesomeGame.GuiMgmt
{
    public class TargetChecker : MonoBehaviour
    {
        [SerializeField]
        PlayerStatistic stat;

        [SerializeField]
        TargetBasic targetBasic;

        [SerializeField]
        GameObject targetPanel;

        [SerializeField]
        Text targetName;

        void Start( ) {
            stat.OnTargetChange += OnTargetChange;
        }

        void OnTargetChange( ) {
            if( stat.CurrentTarget == null ) {
                targetPanel.SetActive( false );
                targetBasic.ChangeTargetToNull( );
                targetName.text = "<No Target>";
            } else {
                targetPanel.SetActive( true );
                targetBasic.ChangeTarget( stat.CurrentTarget );
                targetName.text = stat.CurrentTarget.gameObject.name;
            }
        }
    }
}
