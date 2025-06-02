using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloningTubePawnUpgrade : PieceUpgradeReward
{

    public override PieceType getPieceTarget() {
        return PieceType.Pawn;
    }

    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.promote);
        return changes;
    }

    public virtual void changePromote(ChessPiece p) {
        //create a new pawn
        this.game.getPlayer().createTemporaryPiece(p, this.game.getBoard().getSquareAt(p.startingX, p.startingY));
    }

    public override string getRewardName() {
        return "Cloning Tube";
    }
    public override string getRewardDescription() {
        return "When a pawn promotes, create a new pawn on it's starting square, if that square is empty";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "pawn";
    }
}
