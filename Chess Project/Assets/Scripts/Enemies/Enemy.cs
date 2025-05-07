using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : HostileEntity
{
    protected int minionsFinished; protected bool enemyFinished;


    public List<Minion> minions;
    public Sprite enemySprite;

    public virtual void onEncounterStart(){}

    public override void takeTurn() {
        enemyFinished = false;
        minionsFinished = 0;

        base.takeTurn();

        foreach(Minion m in this.minions) {
            m.takeTurn();
        }
    }

    protected virtual void summonMinions() {
        foreach(Minion m in this.minions) {
            m.gameObject.SetActive(true);
            m.onSummon(this);
        }
    }

    public override void onTurnOver() {
        enemyFinished = true;
        if(minionsFinished == minions.Count) {
            this.game.startPlayerTurn();
        }
    }
    
    public virtual void onMinionFinished() {
        minionsFinished++;
        if(enemyFinished && minionsFinished == minions.Count) {
            this.game.startPlayerTurn();
        }
    }

    public override void returnDamage() {
        game.getBoard().returnDamage(this.position,this.defense);
        foreach(Minion m in this.minions) {
            m.returnDamage();
        }
    }

    public override void onDeath() {
        foreach(Minion m in this.minions) {
            m.onEnemyDeath();
        }
        this.game.getEncounter().onEnemyDefeat();
    }

    //revives all dead minions :)
    public void reviveMinions() {
        foreach(Minion m in this.minions) {
            if(!m.alive) {
                m.onSummon(this);
            }
        }
    }

    public override EntityType getEntityType() {
        return EntityType.Enemy;
    }
}
