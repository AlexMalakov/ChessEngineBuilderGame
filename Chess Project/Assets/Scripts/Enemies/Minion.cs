using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Entity
{

    //minoins also have this lol
    public List<EnemyAction> actionQueue;
    public List<EnemyAction> actionLoop;
    int currentAct = 0;
    public Enemy enemy;
    public bool alive = false;


    public virtual void onSummon(Enemy enemy) {
        gameObject.SetActive(true);
        this.alive = true;
        this.enemy = enemy;
        health = maxHealth;
    }

    public virtual void takeTurn() {
        if(!this.alive) { //PLACE HOLDER, DEAD MINIONS DONT TAKE ACTIONS!
            this.enemy.onMinionFinished();
            return;
        }

        if(currentAct < actionQueue.Count) {
            StartCoroutine(actionQueue[currentAct].takeAction());
        } else {
            StartCoroutine(actionLoop[(currentAct - actionQueue.Count)%actionLoop.Count].takeAction());
        }
        currentAct++;
    }


    public virtual void onTurnOver() {
        this.enemy.onMinionFinished();
    }

    public virtual void returnDamage() {}//minions don't return damage by default


    public override void onDeath() {
        this.alive = false;
        this.position.entity = null;
        this.position = null;
        gameObject.SetActive(false);

    }

    public virtual void onEnemyDeath() {
        this.onDeath();
    }

    public override EntityType getEntityType() {
        return EntityType.Minion;
    }
}
