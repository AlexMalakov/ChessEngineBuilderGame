using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShanksBishopUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getPieceDamage);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Bishop;
    }

    public override Operation changePieceDamage(ChessPiece p, Square target) {
        if(Mathf.Abs(p.position.x - target.x) == 1 && Mathf.Abs(p.position.y - target.y) == 1) {
            return new Operation(OperationTypes.PreAdd, p.damage);
        }
        return new Operation(OperationTypes.Ignore, 0);
    }
}
