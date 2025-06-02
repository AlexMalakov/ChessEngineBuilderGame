using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecklessChargeKnightUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getPieceDamage);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Knight;
    }

    public override Operation changePieceDamage(ChessPiece p, Square target) {
        if(p.effectiveDefense == 0) {
            return new Operation(OperationTypes.Multiply, 250);
        }
        return new Operation(OperationTypes.Ignore, 0);
    }

    public override string getRewardName() {
        return "Reckless Charge";
    }
    public override string getRewardDescription() {
        return "Undefended Knights deal 250% extra damage.";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "knight";
    }
}
