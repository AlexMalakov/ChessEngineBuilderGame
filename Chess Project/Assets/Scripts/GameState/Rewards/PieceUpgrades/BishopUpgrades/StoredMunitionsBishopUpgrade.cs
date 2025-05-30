using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredMunitionsBishopUpgrade : PieceUpgradeReward
{
    public Dictionary<ChessPiece, int> attackCharges = new Dictionary<ChessPiece, int>();
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getPieceDamage);
        changes.Add(PieceMethods.assignEffectiveDefense);
        return changes;
    }
    
    public override PieceType getPieceTarget() {
        return PieceType.Bishop;
    }

    public override void onBind(ChessPiece p) {
        attackCharges.Add(p,-1);
    }

    public override Operation changePieceDamage(ChessPiece piece, Square target) {
        int charges = attackCharges[piece];
        attackCharges[piece]=-1;
        return new Operation(OperationTypes.PostAdd, piece.damage * charges);
    }

    public override Operation changeEffectiveDefense(ChessPiece piece, int defense) {
        attackCharges[piece]+=1;
        return new Operation(OperationTypes.Ignore, 0);
    }

    public override void onEncounterStart() {
        foreach(ChessPiece p in this.attackCharges.Keys) {
            this.attackCharges[p] = -1;
        }
    }

    public override string getRewardName() {
        return "Stored Munitions";
    }
    public override string getRewardDescription() {
        return "Each round that a bishop doesn't attack stores it's base damage. When it attacks, all charges are spent as extra damage.";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "bishop";
    }
}
