using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnAttack : HostileEntityAction
{
    public override IEnumerator act() {
        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y-1) != null
            && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y-1).hasChessPiece()) {
            
            this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y-1).entity.takeDamage(this.opponent.damage);
        }
        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y-1) != null
            && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y-1).hasChessPiece()) {
            
            this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y-1).entity.takeDamage(this.opponent.damage);
        }
        yield return null;
    }
}
