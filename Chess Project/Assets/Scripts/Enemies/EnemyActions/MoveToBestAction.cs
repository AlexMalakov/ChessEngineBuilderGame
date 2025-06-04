using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveToBestAction : HostileEntityAction
{

    public MoveToBestAction(HostileEntity opponent) : base(opponent) {}
    public override IEnumerator act() {
        yield return this.opponent.slide(findBest());
    }

    public virtual Square findBest() {
        Square bestSq = this.opponent.position;
        int best = -1;
        foreach(Square s in this.opponentMoves()) {
            if(this.moveCondition(s) > best || (this.moveCondition(s) == best && Random.Range(0,2) == 0)) {
                best = this.moveCondition(s);
                bestSq = s;
            }
        }
        return bestSq;
    }

    public abstract List<Square> opponentMoves();

    public abstract int moveCondition(Square s);

    //can be used as a mvoe condition
    public int maximizeDamage(Square s, List<Squares> squareAround) {
        int result = 0;
    
        foreach(Square s in squareAround) {
            if(s.entity == null || !s.hasChessPiece()) {
                continue;
            }
            if(s.hasChessPiece()) {
                result++;
            }
            if(((ChessPiece)s.entity).effectiveDefense < this.opponent.damage) {
                result+=2;
            }

            if(((ChessPiece)s.entity).effectiveDefense + s.entity.health <= this.opponent.damage) {
                result++;
            }
        }

        return result;
    }
    //can be used as a move condition
    public int maximizeBoost(Square s, List<Squares> squareAround) {
        int result = 0;
        foreach(Square s in squareAround) {
            if(s.hasHostile()) {
                result++;
            }
        }
        return result;
    }
}
