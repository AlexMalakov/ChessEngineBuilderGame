using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public Projectile projectile;

    public List<EnemyAction> actionQueue;
    public List<EnemyAction> actionLoop;
    int currentAct = 0;
    int minionsFinished; bool enemyFinished;

    public List<ChessPiece> attackers;
    public List<Minion> minions;
    public Sprite enemySprite;
    public EnemyReporter reporter;

    
    public virtual void onEncounterStart() {
        this.reporter.onEncounterStart();
        this.summonMinions();
    }

    public virtual void takeTurn() {
        enemyFinished = true;
        minionsFinished = 0;
        if(currentAct < actionQueue.Count) {
            StartCoroutine(actionQueue[currentAct].takeAction());
        } else {
            StartCoroutine(actionLoop[(currentAct - actionQueue.Count)%actionLoop.Count].takeAction());
        }
        currentAct++;

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

    public virtual void onTurnOver() {
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

    public override void takeDamage(int damage/*, ChessPiece attacker*/) {
        base.takeDamage(damage);
        game.getBoard().returnDamage(this.position,this.defense);
        this.reporter.onStatUpdate();
    }

    public override void onDeath() {
        foreach(Minion m in this.minions) {
            m.onEnemyDeath();
        }
        this.game.getEncounter().onEnemyDefeat();
    }

    public override IEnumerator slide(Square toSquare) {
        this.position.entity = null;
        toSquare.entity = this;
        yield return base.slide(toSquare);
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
