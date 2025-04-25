using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SquareReporter : MonoBehaviour
{
    public Game game;
    public Square square;
    public TMP_Text squareName;
    public TMP_Text squareStats;
    public TMP_Text pieceName;
    public Image squareImage;

    public void onSquareUpdate(Square square) {
        this.square = square;
        int defense = game.getBoard().calculateDefense(square, false);
        squareName.text = square.name;

        if(square.piece != null) {
            if(game.getPremovePieces().Contains(square.piece)) {
                defense = game.getBoard().calculateDefense(square, true);
            }
            squareImage.gameObject.SetActive(true);
            squareImage.sprite = square.piece.activeSprite;
            squareImage.type = Image.Type.Simple;
            squareImage.preserveAspect = false;

            int attack = defense + square.piece.damage;
            pieceName.text = square.piece.name;
            squareStats.text = "health" + square.piece.health + "/" + square.piece.maxHealth + "\ndamage: " + square.piece.damage + "\nattack damage: " + attack + "\ndefense: " + square.piece.defense + "\nincoming defense: "  + defense;
        } else {
            squareImage.gameObject.SetActive(false);
            squareStats.text = "incoming defense: " + defense + "\nincoming damage: " + game.getBoard().calculateDamage(square);
            pieceName.text = "";
        }
    }
}
