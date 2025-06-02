using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//deal base damage to an enemy that can be hit after a move
public class QuickDischarge : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.afterMove);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Bishop;
    }

    public override bool changeAfterMove(ChessPiece piece, Square target) {
        foreach (Square s in piece.getPossibleMoves(true)) {
            if (s.entity != null && !s.hasChessPiece()) {
                s.entity.takeDamage(piece.damage);
            }
        }
        return true;
    }

    public override string getRewardName() {
        return "Quick Discharge";
    }
    public override string getRewardDescription() {
        return "After a bishop moves, it attacks at it's destination, dealing only base damage";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "bishop";
    }
}
