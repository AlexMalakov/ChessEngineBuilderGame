using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFactory : MonoBehaviour
{
    public Game game;
    public GameObject pawnBlueprint;
    public GameObject knightBlueprint;
    public GameObject bishopBlueprint;
    public GameObject rookBlueprint;
    public GameObject queenBlueprint;
    public GameObject kingBlueprint;
    //this may not work with piece changing stats, but that's a future problem
    public ChessPiece createPiece(PieceType pType, int startingX, int startingY) {
        ChessPiece p;
        switch(pType) {
            case PieceType.Pawn:
                p = Instantiate(this.pawnBlueprint).GetComponent<ChessPiece>();
                break;
            case PieceType.Knight:
                p = Instantiate(this.knightBlueprint).GetComponent<ChessPiece>();
                break;
            case PieceType.Bishop:
                p = Instantiate(this.bishopBlueprint).GetComponent<ChessPiece>();
                break;
            case PieceType.Rook:
                p = Instantiate(this.rookBlueprint).GetComponent<ChessPiece>();
                break;
            case PieceType.Queen:
                p = Instantiate(this.queenBlueprint).GetComponent<ChessPiece>();
                break;
            case PieceType.King:
                p = Instantiate(this.kingBlueprint).GetComponent<ChessPiece>();
                break;
            default:
                Debug.Log("INVALID PIECE TYPE! UNABLE TO CREATE PIECE");
                return null;
        }

        foreach(PieceUpgradeReward reward in this.game.getPlayer().myPieceUpgrades) {
            if(reward.getPieceTarget() == PieceType.ChessPiece || reward.getPieceTarget() == pType) {
                p.mountPieceUpgrade(reward);
            }
        }

        p.startingX = startingX; p.startingY = startingY;
        this.game.getBoard().placeEntity(p);
        return p;
    }
}
