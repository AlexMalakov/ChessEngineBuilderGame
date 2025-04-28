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
        return new Operation(OperationTypes.PreAdd, Mathf.Abs(piece.position.x - target.x));
    }
}
