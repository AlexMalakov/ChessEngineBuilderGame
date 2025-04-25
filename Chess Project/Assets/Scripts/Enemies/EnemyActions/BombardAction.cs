using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombardAction : EnemyAction
{
    //launches an attack across every row
    public override IEnumerator act() {
        for(int i = 0; i < this.enemy.game.getBoard().len; i++) {
            yield return this.enemy.slide(this.enemy.game.getBoard().getSquareAt(i, this.enemy.position.y));
            yield return this.enemy.projectile.launch(this.enemy.position, 0, -1, this.enemy.damage);
        }

        int destination = Random.Range(0, this.enemy.game.getBoard().len); //replace with a targeted shot at whatever is the weakest
        yield return this.enemy.slide(this.enemy.game.getBoard().getSquareAt(destination, this.enemy.position.y));
        yield return this.enemy.projectile.launch(this.enemy.position, 0, -1, this.enemy.damage);
    }
}
