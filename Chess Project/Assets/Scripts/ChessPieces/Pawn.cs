using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    public bool hasMoved = false;

    public override List<Square> getPossibleMoves(bool attacking) {
        List<Square> possibleMoves = new List<Square>();

        foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.getMoves]) {
            possibleMoves.AddRange(upgrade.changePossibleMoves(this, false, attacking));
        }

        if(this.game.getBoard().getSquareAt(this.position.x, this.position.y + 1) != null && this.game.getBoard().getSquareAt(this.position.x, this.position.y + 1).entity == null) {
            possibleMoves.Add(this.game.getBoard().getSquareAt(this.position.x, this.position.y + 1));

            if(!hasMoved && this.game.getBoard().getSquareAt(this.position.x, this.position.y + 2) != null && this.game.getBoard().getSquareAt(this.position.x, this.position.y + 2).entity == null)
                possibleMoves.Add(this.game.getBoard().getSquareAt(this.position.x, this.position.y + 2));
        }
        
        if(this.game.getBoard().getSquareAt(this.position.x - 1, this.position.y + 1)!= null && (!this.game.getBoard().getSquareAt(this.position.x - 1, this.position.y + 1).hasChessPiece() && this.game.getBoard().getSquareAt(this.position.x - 1, this.position.y + 1).entity != null)) {
            possibleMoves.Add(this.game.getBoard().getSquareAt(this.position.x - 1, this.position.y + 1));
        }

        if(this.game.getBoard().getSquareAt(this.position.x + 1, this.position.y + 1) != null && (!this.game.getBoard().getSquareAt(this.position.x + 1, this.position.y + 1).hasChessPiece() && this.game.getBoard().getSquareAt(this.position.x + 1, this.position.y + 1).entity != null)) {
            possibleMoves.Add(this.game.getBoard().getSquareAt(this.position.x + 1, this.position.y + 1));
        }
        
        return possibleMoves;
    }


    public override List<Square> getAllMoves() {
        List<Square> possibleMoves = new List<Square>();

        foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.getMoves]) {
            possibleMoves.AddRange(upgrade.changePossibleMoves(this, true, false));
        }

        if(this.game.getBoard().getSquareAt(this.position.x, this.position.y + 1) != null && this.game.getBoard().getSquareAt(this.position.x, this.position.y + 1).entity == null) {
            possibleMoves.Add(this.game.getBoard().getSquareAt(this.position.x, this.position.y + 1));

            if(!hasMoved && this.game.getBoard().getSquareAt(this.position.x, this.position.y + 2) != null && this.game.getBoard().getSquareAt(this.position.x, this.position.y + 2).entity == null)
                possibleMoves.Add(this.game.getBoard().getSquareAt(this.position.x, this.position.y + 2));
        }
        
        if(this.game.getBoard().getSquareAt(this.position.x - 1, this.position.y + 1)!= null && this.game.getBoard().getSquareAt(this.position.x - 1, this.position.y + 1).entity != null) {
            possibleMoves.Add(this.game.getBoard().getSquareAt(this.position.x - 1, this.position.y + 1));
        }

        if(this.game.getBoard().getSquareAt(this.position.x + 1, this.position.y + 1) != null && this.game.getBoard().getSquareAt(this.position.x + 1, this.position.y + 1).entity != null) {
            possibleMoves.Add(this.game.getBoard().getSquareAt(this.position.x + 1, this.position.y + 1));
        }
        
        return possibleMoves;
    }

    public void promote() {
        //FIGURE THIS OUT LATER
    }

    public override bool move(Square c) {
        if(base.move(c)) {
            this.hasMoved = true;
            return true;
        }
        return false;
    }

    public override PieceType getPieceType() {
        return PieceType.Pawn;
    }
}
 