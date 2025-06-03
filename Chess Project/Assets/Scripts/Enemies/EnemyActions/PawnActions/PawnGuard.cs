using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnGuard : HostileEntityAction
{
    public override IEnumerator act() {
        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y-1) != null
                && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y-1).hasHostile()) {
                

            new GuardStatusEffect(this.opponent.defense).onAttatch(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y-1).entity);
        }
        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y-1) != null
            && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y-1).hasHostile()) {
            
            new GuardStatusEffect(this.opponent.defense).onAttatch(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y-1).entity);
        }
        yield return null;
    }
}
