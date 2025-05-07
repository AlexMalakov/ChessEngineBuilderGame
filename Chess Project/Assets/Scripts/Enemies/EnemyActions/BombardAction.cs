using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombardAction : HostileEntityAction
{
    //launches an attack across every row
    public override IEnumerator act() {
        for(int i = 0; i < this.opponent.game.getBoard().len; i++) {
            yield return this.opponent.slide(this.opponent.game.getBoard().getSquareAt(i, this.opponent.position.y));
            yield return this.opponent.projectile.launch(this.opponent.position, 0, -1, this.opponent.damage);
        }

        int destination = Random.Range(0, this.opponent.game.getBoard().len); //replace with a targeted shot at whatever is the weakest
        yield return this.opponent.slide(this.opponent.game.getBoard().getSquareAt(destination, this.opponent.position.y));
        yield return this.opponent.projectile.launch(this.opponent.position, 0, -1, this.opponent.damage);
    }
}
