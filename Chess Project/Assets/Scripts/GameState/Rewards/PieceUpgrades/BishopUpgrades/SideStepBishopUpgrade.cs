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
        return PieceType.Bishop;
    }

    public override List<Square> changePossibleMoves(ChessPiece p, bool defending, bool attacking) {
        List<Square> additionalMoves = new List<Square>();

        if(attacking || defending) {
            return additionalMoves;
        }

        if(checkSquare(p.position.x+1, p.position.y)) {
            additionalMoves.Add(this.game.getBoard().getSquareAt(p.position.x+1, p.position.y));
        }
        if(checkSquare(p.position.x-1, p.position.y)) {
            additionalMoves.Add(this.game.getBoard().getSquareAt(p.position.x-1, p.position.y));
        }
        if(checkSquare(p.position.x, p.position.y+1)) {
            additionalMoves.Add(this.game.getBoard().getSquareAt(p.position.x, p.position.y+1));
        }
        if(checkSquare(p.position.x, p.position.y-1)) {
            additionalMoves.Add(this.game.getBoard().getSquareAt(p.position.x, p.position.y-1));
        }

        return additionalMoves;
    }

    private bool checkSquare(int x, int y) {
        return this.game.getBoard().getSquareAt(x, y) != null && this.game.getBoard().getSquareAt(x, y).entity == null;
    }

    public override string getRewardName() {
        return "Side Step";
    }
    public override string getRewardDescription() {
        return "Bishops can move one square adjacent.";
    }
    public override string getRewardFlavorText() {
        return "";
    }
    public override string getRewardImage() {
        return "bishop";
    }
}
