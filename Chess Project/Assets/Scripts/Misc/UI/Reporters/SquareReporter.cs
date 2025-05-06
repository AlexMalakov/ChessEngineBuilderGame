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

    public void onEncounterStart() {
        foreach(ChessPiece p in this.game.getPieces()) {
            if(p.getPieceType() == PieceType.King) {
                this.onSquareUpdate(p.position);
                return;
            }
        }
        this.onSquareUpdate(this.game.getPieces()[Random.Range(0, this.game.getPieces().Count)].position);
    }

    public void onSquareUpdate(Square square) {
        this.square = square;
        int defense = game.getBoard().calculateDefense(square, false);
        squareName.text = square.squareName;

        if(square.entity == null) { 
            squareImage.gameObject.SetActive(false);
            squareStats.text = "incoming defense: " + defense + "\nincoming damage: " + game.getBoard().calculateDamage(square);
            pieceName.text = "";
        } else if(square.entity.getEntityType() == EntityType.Piece) {
            ChessPiece p = (ChessPiece) square.entity;
            if(game.getPremovePieces().Contains(p)) {
                defense = game.getBoard().calculateDefense(square, true);
            }
            squareImage.gameObject.SetActive(true);
            squareImage.sprite = p.activeSprite;
            squareImage.type = Image.Type.Simple;
            squareImage.preserveAspect = false;

            int attack = defense + p.damage;
            pieceName.text = p.name;
            squareStats.text = "health" + p.health + "/" + p.maxHealth + "\ndamage: " + p.damage + "\nattack damage: " + attack + "\ndefense: " + p.defense + "\nincoming defense: "  + defense;
        } else if(square.entity.getEntityType() == EntityType.Enemy) {

        } else if(square.entity.getEntityType() == EntityType.Minion) {


        }else {
            
        }
    }
}
