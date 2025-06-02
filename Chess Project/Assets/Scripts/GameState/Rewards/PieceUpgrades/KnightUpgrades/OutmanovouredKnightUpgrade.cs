using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutmanovouredKnightUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.move);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Knight;
    }

    public override bool changeMove(ChessPiece p, Square square) {
        if(this.game.playerPremoveTurn) {
            p.effectiveDefense += 5;
        }
        return true;
    }

    public override string getRewardName() {
        return "Outmanovoured";
    }
    public override string getRewardDescription() {
        return "Knights that premove receive 5 defense that round";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "knight";
    }
}
