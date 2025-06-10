using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//multiple different "enemies" instead of just one
public abstract class MultiEnemies : Enemy
{
    bool slain;
    public override void onEncounterStart() {
        this.slain = false;

        this.summonMinions();

        List<HostileEntity> enemies = new List<HostileEntity>();
        enemies.Add(this);
        enemies.AddRange(this.minions);

        //important that this happens before encounterStart
        //actually maybe not
        ((MultiEnemyEncounter)this.game.getEncounter()).setEnemies(enemies);
        
        base.onEncounterStart();
    }

    public override IEnumerator takeTurn() {
        if(!this.slain) {
            yield return base.takeTurn();
        } else {
            minionsFinished = 0;
            foreach(Minion m in this.minions) {
                yield return m.takeTurn();
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
        if(!this.slain) {
            this.position.entity = null;
            this.position = null;
            this.gameObject.SetActive(false);
        }
        this.slain = true;

        foreach(Minion m in this.minions) {
            m.onEnemyDeath();
        }

        foreach(Minion m in this.minions) {
            if(m.alive) {
                return;
            }
        }

        for(int i = this.minions.Count - 1; i >= 0; i--) {
            Destroy(this.minions[i].gameObject);
        }

        this.game.getEncounter().onEnemyDefeat();
        Destroy(this.gameObject);
    }
}

