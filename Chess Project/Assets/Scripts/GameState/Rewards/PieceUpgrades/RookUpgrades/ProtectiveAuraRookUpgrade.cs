using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectiveAuraRookUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getPossibleMoves);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Rook;
    }
    
    public override List<Square> changePossibleMoves(ChessPiece p, bool defending, bool attacking) {
        List<Square> additionalMoves = new List<Square>();
        if(!defending) {
            return additionalMoves;
        }

        Square s = p.position;
        bool passedEntity = false;
        while(true) {
            if(s.game.getBoard().getSquareAt(s.x + 1, s.y) == null) {
                break;
            }

            s = s.game.getBoard().getSquareAt(s.x + 1, s.y);
            if(passedEntity) {
                additionalMoves.Add(s);
            }
            if(!passedEntity && s.entity != null) {
                passedEntity = true;
            }
        }

        passedEntity = false;
        Square s = p.position;
        while(true) {
            if(s.game.getBoard().getSquareAt(s.x - 1, s.y) == null) {
                break;
            }

            s = s.game.getBoard().getSquareAt(s.x - 1, s.y);

            if(passedEntity) {
                additionalMoves.Add(s);
            }
            if(!passedEntity && s.entity != null) {
                passedEntity = true;
            }
        }

        return additionalMoves;
    }
}
