using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveToBestAction : HostileEntityAction
{

    public MoveToBestAction(HostileEntity opponent) : base(opponent) {}
    public override IEnumerator act() {
        Square bestSq = this.opponent.position;
        int best = -1;
        foreach(Square s in this.opponentMoves()) {
            if(this.moveCondition(s) > best || (this.moveCondition(s) == best && Random.Range(0,2) == 0)) {
                best = this.moveCondition(s);
                bestSq = s;
            }
        }

        yield return this.opponent.slide(bestSq);
    }

    public abstract List<Square> opponentMoves();

    public abstract int moveCondition(Square s);
}
