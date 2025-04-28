using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairedUpBishopUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getPieceDamage);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Bishop;
    }

    //changes how a piece's damage is calculated
    public override Operation changePieceDamage(ChessPiece piece, Square target) {
        if(this.checkSquare(piece.position.x+1, piece.position.y)) {
            return new Operation(OperationTypes.Multiply, 100);
        }
        if(this.checkSquare(piece.position.x-1, piece.position.y)) {
            return new Operation(OperationTypes.Multiply, 100);
        }
        if(this.checkSquare(piece.position.x, piece.position.y+1)) {
            return new Operation(OperationTypes.Multiply, 100);
        }
        if(this.checkSquare(piece.position.x, piece.position.y-1)) {
            return new Operation(OperationTypes.Multiply, 100);
        }
        return new Operation(OperationTypes.PreAdd, 0);
    }

    private bool checkSquare(int x, int y) {
        return this.game.getBoard().getSquareAt(x, y) != null && this.game.getBoard().getSquareAt(x, y).hasPiece(PieceType.Bishop);
    }
}
