using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minion : HostileEntity
{
    public Enemy enemy;
    public bool alive = false;
    public bool waveMinion = false;


    public virtual void onSummon(Enemy enemy) {
        gameObject.SetActive(true);
        StartCoroutine(summonAnimation());
        this.alive = true;
        this.enemy = enemy;
        health = maxHealth;
        this.initActions();
    }

    public virtual IEnumerator summonAnimation() {
        float elapsed = 0f;
        Vector3 endPos = this.transform.position;
        Vector3 startPos = endPos + new Vector3(0, 5, 0);

        while(elapsed < this.game.playerAttackDuration) {
            transform.position = Vector3.Lerp(startPos, endPos, elapsed/this.game.playerAttackDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
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
