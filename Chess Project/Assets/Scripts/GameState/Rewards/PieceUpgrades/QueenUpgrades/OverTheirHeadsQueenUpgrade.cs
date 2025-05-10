using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTheirHeadsQueenUpgrade : PieceUpgardeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getPieceDamage);
        changes.Add(PieceMethods.possibleMoves);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Pawn;
    }

    public virtual Operation changePieceDamage(ChessPiece p, Square target){
        int multiplier = 100;
        int delX = target.x - p.position.x;
        delX = delX/Mathf.Abs(delX);
        int delY = target.y - p.position.y
        delY = delY/Mathf.Abs(delY);

        int counter = 1;
        while(true) {
            if(p.game.getBoard().getSquareAt(p.position.x + delX, p.position.y+delY) == null) {
                Debug.Log("ERROR SHOULD NOT BE POSSIBLE!");
                break;
            }
            if(p.game.getBoard().getSquareAt(p.position.x + delX, p.position.y+delY) == target) {
                break;
            }
            if(p.game.getBoard().getSquareAt(p.position.x + delX, p.position.y+delY).hasChessPiece()) {
                multiplier+=10;
            }
        }
        return new Operation(OperationTypes.Multiply, multiplier);
    }

    public virtual List<Square> changePossibleMoves(ChessPiece p, bool defending, bool attacking) {
        List<Square> additionalMoves = new List<Square>();
        if(!attacking) {
            return additionalMoves;
        }

        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{-1,-1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,1});
        offsets.Add(new int[]{0,-1});
        offsets.Add(new int[]{0,1});
        offsets.Add(new int[]{-1,0});
        offsets.Add(new int[]{1,0});

        foreach (int[] offset in offsets) {
            int distance = 1;
            bool passedPiece = false;
            Square s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            while(s != null) {
                if(passedPiece) {
                    possibleMoves.Add(s); 
                }      
                if(s.hasChessPiece() && !passedPiece) {
                    passedPiece = true;
                } else if(s.entity != null) {
                    break;
                }
                distance++;
                s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            }
        }
        return possibleMoves;
    }
    
}
