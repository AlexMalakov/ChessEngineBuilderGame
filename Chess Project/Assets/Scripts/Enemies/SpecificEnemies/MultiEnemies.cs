using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//multiple different "enemies" instead of just one
public class MultiEnemies : Enemy
{
    bool slain;
    public override void onEncounterStart() {
        this.slain = false;
        base.onEncounterStart();
        this.summonMinions();
    }

    public override void takeTurn() {
        if(!this.slain) {
            base.takeTurn();
        } else {
            minionsFinished = 0;
            foreach(Minion m in this.minions) {
                m.takeTurn();
            }
            this.onTurnOver();
        }
    }

    public override void returnDamage() {
        if(!this.slain) {
            base.returnDamage();
        }
    }

    public override void onDeath() { //CHANGE THIS?
        this.slain = true;

        foreach(Minion m in this.minions) {
            m.onEnemyDeath();
        }

        foreach(Minion m in this.minions) {
            if(m.alive) {
                return;
            }
        }

        this.game.getEncounter().onEnemyDefeat();
    }
}

