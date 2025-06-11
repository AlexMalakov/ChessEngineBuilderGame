using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapFrogPawnUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getMoves);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Pawn;
    }

    public override List<Square> changePossibleMoves(ChessPiece p, bool defending, bool attacking) {
        List<Square> additionalMoves = new List<Square>();
        
        if(defending || attacking) {
            return additionalMoves;
        }

        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{-1,-1});
        offsets.Add(new int[]{-1,0});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{0,1});
        offsets.Add(new int[]{1,1});
        offsets.Add(new int[]{1,0});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{0,-1});

        foreach(int[] offset in offsets) {
            Square s = p.game.getBoard().getSquareAt(p.position.x + offset[0], p.position.y + offset[1]);
            if(s != null && s.hasPiece(PieceType.Pawn)) {
                if(p.game.getBoard().getSquareAt(s.x-1, s.y+1) != null && p.game.getBoard().getSquareAt(s.x-1, s.y+1).entity == null) {
                    additionalMoves.Add(p.game.getBoard().getSquareAt(s.x-1, s.y+1));
                }
                if(p.game.getBoard().getSquareAt(s.x+1, s.y+1) != null && p.game.getBoard().getSquareAt(s.x+1, s.y+1).entity == null) {
                    additionalMoves.Add(p.game.getBoard().getSquareAt(s.x+1, s.y+1));
                }
            }
        }
        return additionalMoves;
    }

    public override string getRewardName() {
        return "Leap Frog";
    }
    public override string getRewardDescription() {
        return "Pawns can move to squares diagonally in front of adjacent pawns.";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "pawn";
    }

}
