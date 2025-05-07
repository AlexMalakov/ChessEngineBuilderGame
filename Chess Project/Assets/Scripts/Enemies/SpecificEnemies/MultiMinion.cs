using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//minion that is essentially just as important as an enemy
public class MultiMinion : Minion
{
    public override void onDeath() {
        base.onDeath();
        checkIfAllDead();
    }

    private void checkIfAllDead() {
        foreach(Minion m in this.enemy.minions) {
            if(m.alive) {
                return;
            }
        }

        enemy.onDeath();
    }

    public override void onEnemyDeath() {} //we don't care cuz we're a multi minion

    public override void returnDamage() {
        game.getBoard().returnDamage(this.position,this.defense);
    }
}