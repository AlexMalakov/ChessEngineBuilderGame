using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightHop : HostileEntityAction
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
            if(this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]) == null 
                || this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]).entity != null) {
                squares.Add(this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]));
            }
        }

        yield return this.opponent.slide(squares[Random.Range(0, squares.Count)]);
    }
}
