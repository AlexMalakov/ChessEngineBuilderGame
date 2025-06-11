using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AQuickPickMeUpPawnUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.onSacrifice);
        return changes;
    }
    
    public override PieceType getPieceTarget() {
        return PieceType.Pawn;
    }

    public override void changeOnSacrifice(ChessPiece p, Square target){
        foreach(ChessPiece pp in this.game.getPieces()) {
            if(pp.getPieceType() == PieceType.King) {
                pp.health = Mathf.Min(pp.maxHealth, pp.health + 1);
            }
        }
    }

    public override string getRewardName() {
        return "Quick Pick-Me-Up";
    }
    public override string getRewardDescription() {
        return "On a pawn sacrifice, your king is healed for 1 hp";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "pawn";
    }
}
