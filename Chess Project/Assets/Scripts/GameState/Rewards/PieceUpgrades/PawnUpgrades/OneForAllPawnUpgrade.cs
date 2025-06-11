using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneForAllPawnUpgrade : PieceUpgradeReward
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
        int count = -1;//-1 since this pawn doesn't die till after the sacrifice
        foreach(ChessPiece pp in this.game.getPieces()) {
            if(pp.getPieceType() == PieceType.Pawn) {
                count++;
            }
        }

        target.entity.takeDamage(count);
    }

    public override string getRewardName() {
        return "One for All";
    }
    public override string getRewardDescription() {
        return "On pawn sacrifice, deal additional damage for each living pawn";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "pawn";
    }
}
