using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatTheForkKnightUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getPieceDamage);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Knight;
    }


    public override Operation changePieceDamage(ChessPiece p, Square target){
        int counter = 0;
        foreach(Square s in p.getPossibleMoves(true)) {
            if(s.hasHostile()) {
                counter++;
            }
        }
        return new Operation(OperationTypes.Multiply, counter * 100);
    }
    public override string getRewardName() {
        return "What The Fork?";
    }
    public override string getRewardDescription() {
        return "Knights deal extra damage for every enemy they can target during their attack";
    }
    public override string getRewardFlavorText() {
        return "this is some forking bull-shirt!";
    }
    public override string getRewardImage() {
        return "knight";
    }
}
