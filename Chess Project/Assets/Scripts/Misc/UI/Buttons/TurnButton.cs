using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnButton : MonoBehaviour
{
    public Game game;
    public Button button;
    public Image panel;
    public TMP_Text buttonContents;

    public void Start() {
        Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(onButtonClick);
    }

    public void onTurnUpdate() {
        if(!game.playerPremoveTurn && !game.playersTurn) {
            buttonContents.text = "";
            panel.color = Color.gray;
        } else if(game.playerPremoveTurn) {
            buttonContents.text = "end premoves";
            panel.color = Color.green;
        } else if(game.playersTurn) {
            buttonContents.text = "end moves";
            panel.color = Color.green;
        }
    }

    public void onButtonClick()
    {
        if(game.playerPremoveTurn || game.playersTurn) {
            game.forceTurnOver();
        }
    }
}
