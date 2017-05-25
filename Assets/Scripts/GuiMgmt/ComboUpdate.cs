using System.Collections;
using System.Collections.Generic;
using AwesomeGame.PlayerMgmt;
using UnityEngine;
using UnityEngine.UI;

namespace AwesomeGame.GuiMgmt
{
    public class ComboUpdate : MonoBehaviour
    {
        public PlayerStatistic Stats;
        ComboHandler comboHandler;
        Text text;

        void Start( ) {
            comboHandler = Stats.ComboHandler;
            comboHandler.OnComboUpdate += UpdateText;
            text = GetComponent<Text>( );
            UpdateText( );
        }

        public void UpdateText( ) {
            text.text = "Combo: " + comboHandler.CurrentMultiplier.ToString( "f2" );
        }
    }
}

