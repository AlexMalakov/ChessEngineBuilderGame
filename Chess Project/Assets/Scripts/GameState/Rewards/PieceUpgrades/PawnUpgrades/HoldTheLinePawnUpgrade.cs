using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTheLinePawnUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getDefense);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Pawn;
    }

    public override Operation changeDefense(ChessPiece p) {
        int chainSize = followPawnChain(p.position, p.game);
        return new Operation(OperationTypes.PreAdd, chainSize);
    }
    
    public int followPawnChain(Square s, Game g) {
        int chain = 0;
        if(g.getBoard().getSquareAt(s.x-1,s.y-1) != null && g.getBoard().getSquareAt(s.x-1,s.y-1).hasPiece(PieceType.Pawn)) {
            chain = chain + 1 + followPawnChain(g.getBoard().getSquareAt(s.x-1,s.y-1), g);
        }

        if(g.getBoard().getSquareAt(s.x+1,s.y-1) != null && g.getBoard().getSquareAt(s.x+1,s.y-1).hasPiece(PieceType.Pawn)) {
            chain = chain + 1 + followPawnChain(g.getBoard().getSquareAt(s.x+1,s.y-1), g);
        }

        return chain;
    }
}
