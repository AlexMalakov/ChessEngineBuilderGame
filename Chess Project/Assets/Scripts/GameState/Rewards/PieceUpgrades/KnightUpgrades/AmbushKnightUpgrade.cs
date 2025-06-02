using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushKnightUpgrade : PieceUpgradeReward
{
    public Dictionary<ChessPiece, ChessPiece> toSwap = new Dictionary<ChessPiece, ChessPiece>();
    public Dictionary<ChessPiece, Square> swapping = new Dictionary<ChessPiece, Square>();
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getMoves);
        changes.Add(PieceMethods.move);
        changes.Add(PieceMethods.afterMove);
        changes.Add(PieceMethods.getPieceDamage);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Knight;
    }

    public override List<Square> changePossibleMoves(ChessPiece p, bool defending, bool attacking) {
        if(defending || attacking) {
            return new List<Square>();
        }
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
            if(this.game.getBoard().getSquareAt(p.position.x + offset[0], p.position.y + offset[1]) != null
                && this.game.getBoard().getSquareAt(p.position.x + offset[0], p.position.y + offset[1]).hasChessPiece()) {
                additionalMoves.Add(this.game.getBoard().getSquareAt(p.position.x + offset[0], p.position.y + offset[1]));
            }
        }

        return additionalMoves;
    }

    public override bool changeMove(ChessPiece p, Square s) {
        if(swapping.ContainsKey(p)) {
            swapping.Remove(p);
        }
        if(s.hasChessPiece()) {
            this.toSwap[p] = (ChessPiece)s.entity;
            this.swapping[p] = s;
        }
        return true;
    }

    public override bool changeAfterMove(ChessPiece p, Square s) {
        if(this.toSwap.ContainsKey(p)) {
            StartCoroutine(this.toSwap[p].move(this.swapping[p]));
            this.toSwap.Remove(p);
        }
        return true;
    }

    public override Operation changePieceDamage(ChessPiece p, Square target) {
        if(this.swapping.ContainsKey(p)) {
            return new Operation(OperationTypes.Multiply, 125);
        }
        return new Operation(OperationTypes.Ignore, 0);
    }

    public override string getRewardName() {
        return "Ambush";
    }
    public override string getRewardDescription() {
        return "Knights can swap places with other pieces. While swapped, receive a damage boost for it's next attack.";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "knight";
    }
}
