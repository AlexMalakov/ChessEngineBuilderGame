using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{

    public override List<Square> getPossibleMoves(bool attacking) {
        List<Square> possibleMoves = new List<Square>();

        if(this.pieceUpgrades.ContainsKey(PieceMethods.getMoves)) {
            foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.getMoves]) {
                possibleMoves.AddRange(upgrade.changePossibleMoves(this, false, attacking));
            }
        }

        List<int[]> moves = new List<int[]>();
        moves.Add(new int[]{2,-1});
        moves.Add(new int[]{2,1});
        moves.Add(new int[]{1,2});
        moves.Add(new int[]{-1,2});
        moves.Add(new int[]{-2,-1});
        moves.Add(new int[]{-2,1});
        moves.Add(new int[]{-1,-2});
        moves.Add(new int[]{1,-2});

        foreach (int[] move in moves) {
            Square s = this.game.getBoard().getSquareAt(this.position.x + move[0], this.position.y + move[1]);
            if(s != null && (s.entity == null || s.entity.getEntityType() != EntityType.Piece)) {
                possibleMoves.Add(s);
            }
        }
        return possibleMoves;
    }

    public override List<Square> getDefensiveMoves() {
        List<Square> possibleMoves = new List<Square>();

        if(this.pieceUpgrades.ContainsKey(PieceMethods.getMoves)) {
            foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.getMoves]) {
                possibleMoves.AddRange(upgrade.changePossibleMoves(this, true, false));
            }
        }

        List<int[]> moves = new List<int[]>();
        moves.Add(new int[]{2,-1});
        moves.Add(new int[]{2,1});
        moves.Add(new int[]{1,2});
        moves.Add(new int[]{-1,2});
        moves.Add(new int[]{-2,-1});
        moves.Add(new int[]{-2,1});
        moves.Add(new int[]{-1,-2});
        moves.Add(new int[]{1,-2});

        foreach (int[] move in moves) {
            Square s = this.game.getBoard().getSquareAt(this.position.x + move[0], this.position.y + move[1]);
            if(s != null) {
                possibleMoves.Add(s);
            }
        }
        return possibleMoves;
    }


    public override PieceType getPieceType() {
        return PieceType.Knight;
    }
}
