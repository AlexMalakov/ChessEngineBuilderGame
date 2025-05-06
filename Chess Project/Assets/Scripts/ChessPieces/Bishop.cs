using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    public override List<Square> getPossibleMoves(bool attacking) {
        List<Square> possibleMoves = new List<Square>();

        if(this.pieceUpgrades.ContainsKey(PieceMethods.getMoves)) {
            foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.getMoves]) {
                possibleMoves.AddRange(upgrade.changePossibleMoves(this, false, attacking));
            }
        }

        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{-1,-1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,1});

        foreach (int[] offset in offsets) {
            int distance = 1;
            Square s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            while(s != null) {
                if(s.entity != null) {
                    if(s.entity.getEntityType() == EntityType.Piece) {
                        break;
                    }
                    possibleMoves.Add(s);
                    break;
                }
                possibleMoves.Add(s);
                distance++;
                s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
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

        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{-1,-1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,1});

        foreach (int[] offset in offsets) {
            int distance = 1;
            Square s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            while(s != null) {
                possibleMoves.Add(s);        
                if(s.entity != null) {
                    break;
                }
                distance++;
                s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            }
        }
        return possibleMoves;
    }

    public override PieceType getPieceType() {
        return PieceType.Bishop;
    }
}
