using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : HostileEntityAction
{
    public override IEnumerator act() {
        List<Square> squares = new List<Square>();
        List<int[]> moves = new List<int[]>();
        moves.Add(new int[]{2,-1});
        moves.Add(new int[]{2,1});
        moves.Add(new int[]{1,2});
        moves.Add(new int[]{-1,2});
        moves.Add(new int[]{-2,-1});
        moves.Add(new int[]{-2,1});
        moves.Add(new int[]{-1,-2});
        moves.Add(new int[]{1,-2});

        Square s = this.opponent.position;
        
        foreach(int[] m in moves) {
            if(this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]) != null 
                || this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]).hasChessPiece()) {
                
                this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]).entity.takeDamage(this.opponent.damage);
            }
        }

        yield return null;
    }
}
