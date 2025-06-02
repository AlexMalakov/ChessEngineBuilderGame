using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTheInitiativePawnUpgrade : PieceUpgradeReward
{
    public override PieceType getPieceTarget() {
        return PieceType.Pawn;
    }

    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.move);
        return changes;
    }

    public override bool changeAfterMove(ChessPiece p, Square square) {
        ((Pawn)p).hasMoved = false;
        return true;
    }

    public override string getRewardName() {
        return "Take Initiative";
    }
    public override string getRewardDescription() {
        return "The first time a pawn moves, it can perform it's 2-space move again.";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "pawn";
    }
}
