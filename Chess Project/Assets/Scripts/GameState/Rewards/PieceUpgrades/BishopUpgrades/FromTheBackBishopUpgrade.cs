using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromTheBackBishopUpgrade : PieceUpgradeReward
{
    public ov erride List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getPieceDamage);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Bishop;
    }

    //changes how a piece's damage is calculated
    public override Operation changePieceDamage(ChessPiece piece, Square target) {
        if(piece.position.y == 0 || piece.position.y == 1) {
            return new Operation(OperationTypes.Multiply, 25);
        }
        return new Operation(OperationTypes.Ignore, 0);
    }
}
