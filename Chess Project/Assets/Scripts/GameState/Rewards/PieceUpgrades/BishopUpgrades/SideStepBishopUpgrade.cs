using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideStepBishopUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getMoves);
        return changes;
    }
    
    public override PieceType getPieceTarget() {
        return PieceType.ChessPiece;
    }

    public override List<Square> changePossibleMoves(ChessPiece p, bool all) {
        List<Square> additionalMoves = new List<Square>();

        if(checkSquare(p.position.x+1, p.position.y, all)) {
            additionalMoves.Add(this.game.getBoard().getSquareAt(p.position.x+1, p.position.y));
        }
        if(checkSquare(p.position.x-1, p.position.y, all)) {
            additionalMoves.Add(this.game.getBoard().getSquareAt(p.position.x-1, p.position.y));
        }
        if(checkSquare(p.position.x, p.position.y+1, all)) {
            additionalMoves.Add(this.game.getBoard().getSquareAt(p.position.x, p.position.y+1));
        }
        if(checkSquare(p.position.x, p.position.y-1, all)) {
            additionalMoves.Add(this.game.getBoard().getSquareAt(p.position.x, p.position.y-1));
        }

        return additionalMoves;
    }

    private bool checkSquare(int x, int y, bool all) {
        return this.game.getBoard().getSquareAt(x, y) != null && ((this.game.getBoard().getSquareAt(x, y).entity == null) || (all && this.game.getBoard().getSquareAt(x, y).entity.getEntityType() == EntityType.Piece));
    }
}
