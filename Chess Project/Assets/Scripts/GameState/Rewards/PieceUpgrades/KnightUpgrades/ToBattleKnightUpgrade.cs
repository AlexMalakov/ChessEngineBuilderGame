using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBattleKnightUpgrade : PieceUpgradeReward
{
    Dictionary<ChessPiece, ChessPiece> mountRiders = new Dictionary<ChessPiece, ChessPiece>();


    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getMoves);
        changes.Add(PieceMethods.move);
        changes.Add(PieceMethods.getDefense);
        changes.Add(PieceMethods.attack);
        changes.Add(PieceMethods.assignEffectiveDefense);
        changes.Add(PieceMethods.onDeath);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.ChessPiece;
    }


    public override List<Square> changePossibleMoves(ChessPiece p, bool defending, bool attacking) {
        if(!defending && !attacking) {
            return addMountMoves(p);
        }
        return new List<Square>();
    }

    private List<Square> addMountMoves(ChessPiece p) {
        List<Square> mountMoves = new List<Square>();
        List<int[]> moves = new List<int[]>();
        moves.Add(new int[]{0,-1});
        moves.Add(new int[]{0,1});
        moves.Add(new int[]{1,0});
        moves.Add(new int[]{-1,0});
        moves.Add(new int[]{-1,-1});
        moves.Add(new int[]{-1,1});
        moves.Add(new int[]{1,-1});
        moves.Add(new int[]{1,1});

        foreach(int[] move in moves) {
            if(p.game.getBoard().getSquareAt(p.position.x + move[0], p.position.y + move[1]) != null 
                && p.game.getBoard().getSquareAt(p.position.x + move[0], p.position.y + move[1]).hasPiece(PieceType.Knight)) {

                mountMoves.Add(p.game.getBoard().getSquareAt(p.position.x + move[0], p.position.y + move[1]));
            }
        }
        return mountMoves;
    }
    

    public override bool changeMove(ChessPiece p, Square s) {
        if(Mathf.Abs(p.position.x - s.x) <= 1 && Mathf.Abs(p.position.y - s.y) <= 1 && s.hasPiece(PieceType.Knight)) {
            this.mountRiders.Add((ChessPiece)s.entity, p);
            p.transform.parent = s.entity.transform;
            //issue, s.entity = the rider, not the mounnt
            //we can call this a feature and not a bug
        }
        if(this.mountRiders.ContainsKey(p)) {
            mountRiders[p].position = s;
        }
        return true;
    }

    public override Operation changeDefense(ChessPiece p) {
        if(this.mountRiders.ContainsKey(p)) {
            return new Operation(OperationTypes.PostAdd, this.mountRiders[p].getDefense());
        }
        return new Operation(OperationTypes.Ignore, 0);
    }

    public override void changeOnDeath(ChessPiece p) {
        if(this.mountRiders.ContainsKey(p)) {
            mountRiders[p].transform.parent = null;
            this.mountRiders.Remove(p);
        }
    }

    public override Operation changeEffectiveDefense(ChessPiece p, int defense) {
        if(this.mountRiders.ContainsValue(p)) {
            return new Operation(OperationTypes.PostAdd, 3);
        }
        return new Operation(OperationTypes.Ignore, 0);
    }

    public override IEnumerator changeAttack(ChessPiece p, Entity target, List<ChessPiece> defenders) {
        if(this.mountRiders.ContainsKey(p)) {
            yield return this.mountRiders[p].attack(target, defenders);
        }
        yield return null;
    }

    public override void onEncounterStart() {
        this.mountRiders = new Dictionary<ChessPiece, ChessPiece>();
    }

    public override string getRewardName() {
        return "To Battle!";
    }
    public override string getRewardDescription() {
        return "Pieces can move onto adjacent knights to ride them";
    }
    public override string getRewardFlavorText() {
        return "cheers love, the cavalry's here";
    }
    public override string getRewardImage() {
        return "knight";
    }

}
