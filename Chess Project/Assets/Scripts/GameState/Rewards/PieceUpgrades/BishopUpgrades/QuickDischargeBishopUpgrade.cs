using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//deal base damage to an enemy that can be hit after a move
public class QuickDischarge : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.move);
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
}
