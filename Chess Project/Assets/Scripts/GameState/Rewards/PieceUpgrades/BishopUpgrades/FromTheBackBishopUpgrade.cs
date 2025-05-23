using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromTheBackBishopUpgrade : PieceUpgradeReward
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
        if(piece.position.y == 0 || piece.position.y == 1) {
            return new Operation(OperationTypes.Multiply, 25);
        }
        return new Operation(OperationTypes.Ignore, 0);
    }

    public override string getRewardName() {
        return "From the Back";
    }
    public override string getRewardDescription() {
        return "grants 25% increased damage to a bishop when attacking from rank 1 or 2";
    }
    public override string getRewardFlavorText() {
        return "I'll just be back here...";
    }
    public override string getRewardImage() {
        return "bishop";
    }
}
