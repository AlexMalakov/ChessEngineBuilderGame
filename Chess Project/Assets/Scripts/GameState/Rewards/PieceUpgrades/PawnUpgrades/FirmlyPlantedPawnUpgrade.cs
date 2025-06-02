using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirmlyPlantedPawnUpgrade : PieceUpgradeReward
{
    public override PieceType getPieceTarget() {
        return PieceType.Pawn;
    }

    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.assignEffectiveDefense);
        return changes;
    }

    public virtual Operation changeEffectiveDefense(ChessPiece p, int defense) {
        if(p is Pawn && ((Pawn)p).hasMoved) {
            return new Operation(OperationTypes.PreAdd, 3);
        }
        return new Operation(OperationTypes.Ignore, 0);
    }

    public override string getRewardName() {
        return "Firmly Planted";
    }
    public override string getRewardDescription() {
        return "Pawns that have not moved receive bonus defense";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "pawn";
    }
}
