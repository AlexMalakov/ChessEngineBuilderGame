using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategyCycleButton : MonoBehaviour
{
    public King king;
    public Game game;
    public Button button;
    public GameObject buttonObj;

    public void Start() {
        Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(onButtonClick);
    }

    public void onEncounterStart() {
        foreach(ChessPiece p in this.game.getPieces()) {
            if(p.getPieceType() == PieceType.King && ((King)p).getStrategies().Count > 1) {
                this.king = (King)p;
                this.buttonObj.SetActive(true);
                return;
            }
        }
        this.buttonObj.SetActive(false);
    }

    public void onButtonClick()
    {
        if(this.king.canSwapStrategy()) {
            this.king.swapStrategy();
        }
    }

}
