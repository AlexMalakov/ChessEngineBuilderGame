using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HauntedManorRookUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getMoves);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Rook;
    }
    
    public override List<Square> changePossibleMoves(ChessPiece p, bool defending, bool attacking) {
        List<Square> additionalMoves = new List<Square>();
        if(defending || attacking) {
            return additionalMoves;
        }

        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{1,0});
        offsets.Add(new int[]{-1,0});
        offsets.Add(new int[]{0,1});
        offsets.Add(new int[]{0,-1});

        foreach(int[] offset in offsets) {
            Square s = p.position;
            bool passedEntity = false;
            while(true) {
                if(p.game.getBoard().getSquareAt(s.x + offset[0], s.y + offset[1]) == null) {
                    break;
                }

                s = p.game.getBoard().getSquareAt(s.x + offset[0], s.y + offset[1]);
                if(!passedEntity && s.entity != null) {
                    passedEntity = true;
                }

                if(passedEntity && s.entity == null) {
                    additionalMoves.Add(s);
                }
            }
        }
        return additionalMoves;
    }

    public override string getRewardName() {
        return "Haunted Manor";
    }
    public override string getRewardDescription() {
        return "Rooks can move through friendly pieces, and grants those pieces guard";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "rook";
    }
}
