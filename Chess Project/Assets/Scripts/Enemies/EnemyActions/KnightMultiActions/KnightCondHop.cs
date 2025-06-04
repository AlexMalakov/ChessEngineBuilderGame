using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightCondHop : MoveToBestAction
{
    public bool guarding;

    public override List<Square> opponentMoves() {
        return this.getSquaresAround(this.opponent.position, false);
    }

    private List<Square> getSquaresAround(Square s, bool canHaveEntity) {
        List<Square> hops = new List<Square>();
        List<int[]> moves = new List<int[]>();
        moves.Add(new int[]{2,-1});
        moves.Add(new int[]{2,1});
        moves.Add(new int[]{1,2});
        moves.Add(new int[]{-1,2});
        moves.Add(new int[]{-2,-1});
        moves.Add(new int[]{-2,1});
        moves.Add(new int[]{-1,-2});
        moves.Add(new int[]{1,-2});

        foreach(int[] move in moves) {
            Square posMove = this.opponent.game.getBoard().getSquareAt(s.x + move[0], s.y + move[1]);
            if(posMove != null && (posMove.entity == null || canHaveEntity)) {
                hops.Add(posMove);
            }
        }
        return hops;
    }

    public override int moveCondition(Square sq) {
        if(guarding) {
            return this.maximizeBoost(sq, this.getSquaresAround(sq, true));
        } else {
            return this.maximizeDamage(sq, this.getSquaresAround(sq, true));
        }
    }
    
}
