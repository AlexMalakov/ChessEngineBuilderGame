using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionsButton : MonoBehaviour
{
    //idea: 
    //certain things will want to use actions (king strat, dismounting, etc)
    //should be controlled here
    //thought is that if a reward has an action, it gets added to this method, which then goes thorugh all actions
    //default should be a stances current action, backup is a selected pieces action
    //will probably be redesingefd in the future regardless


    public Game game;
    // public Button button;
    // public Image panel;
    // public GameObject actionButton;
    // public TMP_Text buttonContents;

    // public void Start() {
    //     Button btn = button.GetComponent<Button>();
	// 	btn.onClick.AddListener(onButtonClick);
    // }

    // public void onTurnUpdate() {
    //     if(!game.playerPremoveTurn && !game.playersTurn) {
    //         buttonContents.text = "";
    //         panel.color = Color.gray;
    //     } else if(game.playerPremoveTurn) {
    //         buttonContents.text = "end premoves";
    //         panel.color = Color.green;
    //     } else if(game.playersTurn) {
    //         buttonContents.text = "end moves";
    //         panel.color = Color.green;
    //     }
    // }

    // public void onButtonClick()
    // {

    // }


    // public void onSquarePress(Square sq) {
    //     if(sq.hasChessPiece() && sq.hasPiece(PieceType.King) && ((King) sq.entity).canSwapStrategy()){
    //         actionButton.SetActive(true);

    //         return;
    //     }

    //     actionButton.SetActive(false);
    // }

}
