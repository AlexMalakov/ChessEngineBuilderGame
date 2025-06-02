using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormholeSteedKnightUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getMoves);
        return changes;
    }

    public override List<Square> changePossibleMoves(ChessPiece p, bool defending, bool attacking) {
        List<Square> additionalMoves = new List<Square>();
        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{1,2});
        offsets.Add(new int[]{1,-2});
        offsets.Add(new int[]{-1,2});
        offsets.Add(new int[]{-1,-2});
        offsets.Add(new int[]{2,-1});
        offsets.Add(new int[]{2,1});
        offsets.Add(new int[]{-2,-1});
        offsets.Add(new int[]{-2,1});

        foreach(int[] offset in offsets) {
            if(this.game.getBoard().getSquareAt(p.position.x + offset[0], p.position.y + offset[1]) == null) {
                int len = this.game.getBoard().len; int height = this.game.getBoard().height;
                int x = (((p.position.x + offset[0]) % len) + len) % len;
                int y = (((p.position.y + offset[1]) % height) + height) % height;
                additionalMoves.Add(this.game.getBoard().getSquareAt(x, y));
            }
        }

        return additionalMoves;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Knight;
    }

    public override string getRewardName() {
        return "Wormhole Steed";
    }
    public override string getRewardDescription() {
        return "Knights can move, attack or defend off the board, being transported to the opposite side.";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "knight";
    }
}
