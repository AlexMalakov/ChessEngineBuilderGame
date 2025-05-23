using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBishopUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getPieceDamage);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Bishop;
    }

    //changes how a piece's damage is calculated
    public override Operation changePieceDamage(ChessPiece piece, Square target) {

        int distance = 0;
        int hit = 0;
        List<Square> attackMoves = piece.getPossibleMoves(true);
        for(hit = 0; hit < attackMoves.Count; hit++) {
            if(attackMoves[hit] == target) {
                break;
            }
        }

        for(int i = hit; i >= 0; i--) {

            if(Mathf.Abs(piece.position.x - attackMoves[i].x) <= 1 && Mathf.Abs(piece.position.y - attackMoves[i].y) <= 1) {
                break;
            }
            distance++;
        }
        return new Operation(OperationTypes.PreAdd, distance);
    }

    public override string getRewardName() {
        return "Sniper";
    }
    public override string getRewardDescription() {
        return "Bishops deal increased damage for every square it's attack travels";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "bishop";
    }
}
