using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : HostileEntity
{
    public Enemy enemy;
    public bool alive = false;
    public bool waveMinion = false;


    public virtual void onSummon(Enemy enemy) {
        gameObject.SetActive(true);
        this.alive = true;
        this.enemy = enemy;
        health = maxHealth;
    }

    public override void takeTurn() {
        if(!this.alive) { //PLACE HOLDER, DEAD MINIONS DONT TAKE ACTIONS!
            this.enemy.onMinionFinished();
            return;
        }
        base.takeTurn();
    }


    public override void onTurnOver() {
        this.enemy.onMinionFinished();
    }

    public override void returnDamage() {}//minions don't return damage by default


    public override void onDeath() {
        this.alive = false;
        this.position.entity = null;
        this.position = null;
        gameObject.SetActive(false);
        if(this.waveMinion) {
            this.enemy.minions.Remove(this);
            Destroy(this.gameObject);
        }

    }

    public virtual void onEnemyDeath() {
        this.onDeath();
        if(!this.waveMinion) {
            Destroy(this.gameObject);
        }
    }

    public override EntityType getEntityType() {
        return EntityType.Minion;
    }
}
