using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullSpeedBishopUpgrade : PieceUpgradeReward
{
    public Dictionary<ChessPiece, bool> hasMoved = new Dictionary<ChessPiece, bool>();
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.move);
        changes.Add(PieceMethods.getPieceDamage);
        changes.Add(PieceMethods.assignEffectiveDefense);
        return changes;
    }
    
    public override PieceType getPieceTarget() {
        return PieceType.Bishop;
    }

    public override void onBind(ChessPiece p) {
        hasMoved.Add(p,false);
    }

    public override bool changeMove(ChessPiece p, Square destination) {
        hasMoved[p] = true;
        return false;
    }

    public override Operation changePieceDamage(ChessPiece piece, Square target) {
        if(hasMoved[piece]) {
            return new Operation(OperationTypes.Multiply, 25);
        }
        return new Operation(OperationTypes.Ignore, 0);
    }

    public override Operation changeEffectiveDefense(ChessPiece piece, int defense) {
        hasMoved[piece]=false;
        return new Operation(OperationTypes.Ignore, 0);
    }

    public override string getRewardName() {
        return "Full Speed";
    }
    public override string getRewardDescription() {
        return "Bishops deal 25% increased if they have moved this round";
    }
    public override string getRewardFlavorText() {
        return "gotta go fast";
    }
    public override string getRewardImage() {
        return "bishop";
    }
}
