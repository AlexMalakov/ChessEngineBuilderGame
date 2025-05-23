using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//possible bug to consider: if hats feature happens, if a bishop gets a queen hat this might be a problem. A solution
//for the future could be to have 2 getPieceType methods: a getPieceType and getPieceIdentity, which allows some tomfoolery but not too much
public class QueenOfPietyBishopUpgrade : PieceUpgradeReward
{

    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.attack);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Bishop;
    }

    public override IEnumerator changeAttack(ChessPiece bishop, Entity target, List<ChessPiece> defenders) {
        foreach(ChessPiece p in this.game.getPieces()) {
            if(p.getPieceType() == PieceType.Queen && p.getDefensiveMoves().Contains(bishop.position)) {
                yield return p.attack(target, new List<ChessPiece>());
            }
        }
    }

    public override string getRewardName() {
        return "Queen of Piety";
    }
    public override string getRewardDescription() {
        return "After a bishop attacks, all queens attack the bishops target";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "bishop";
    }
}
