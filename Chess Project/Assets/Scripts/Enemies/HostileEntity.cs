using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileEntity : Entity
{
    public Projectile projectile;

    public List<HostileEntityAction> actionQueue;
    public List<HostileEntityAction> actionLoop;
    int currentAct = 0;

    public virtual void takeTurn() {
        if(currentAct < actionQueue.Count) {
            StartCoroutine(actionQueue[currentAct].takeAction());
        } else {
            StartCoroutine(actionLoop[(currentAct - actionQueue.Count)%actionLoop.Count].takeAction());
        }
        currentAct++;
    }

    //communicate to minions/enemyies that the turn is over, and then once its fully over let the game + encounter know
    public abstract void onTurnOver();

    public override void takeDamage(int damage/*, ChessPiece attacker*/) {
        base.takeDamage(damage);
        this.game.getEncounter().encReporter.onStatUpdate();
    }

    public abstract void returnDamage();
}
