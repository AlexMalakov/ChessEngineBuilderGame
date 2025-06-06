using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Retreat : HostileEntityAction
{
    public Retreat(HostileEntity opponent) : base(opponent) {}
    public override IEnumerator act() {
        int minDamage = this.opponent.game.getBoard().calculateDamage(this.opponent.position);
        Square best = this.opponent.position;

        foreach(Square s in this.getRetreatableSquares()) {
            int incoming = this.opponent.game.getBoard().calculateDamage(s);
            if(minDamage >= incoming) {
                minDamage = incoming;
                best = s;
            }
        }

        yield return this.opponent.slide(best);
    }

    public abstract List<Square> getRetreatableSquares();
}
