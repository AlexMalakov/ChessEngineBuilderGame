using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMove : HostileEntityAction
{
    public int promotionDamage = 5;
    public override IEnumerator act() {
        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x, this.opponent.position.y-1) != null
            && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x, this.opponent.position.y-1).entity == null) {
            
            if(this.opponent.position.y-1 == 0) {
                foreach(ChessPiece p in this.opponent.game.getPieces()) {
                    p.takeDamage(promotionDamage);
                }
            }
            yield return this.opponent.slide(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x, this.opponent.position.y-1));
        }
        yield return null;
    }
}
