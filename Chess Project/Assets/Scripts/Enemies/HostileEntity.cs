using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileEntity : Entity
{
    public List<GameObject> projectiles;

    public List<HostileEntityAction> actionQueue;
    public List<HostileEntityAction> actionLoop;
    int currentAct = 0;

    public abstract void initActions();

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

    public virtual void launchProjectile(int projectileNumber, int deltaX, int deltaY, int projectileDamage) {
        GameObject proj = Instantiate(projectiles[projectileNumber]); //consider object pooling? at the moment not needed but maybe in the future
        StartCoroutine(proj.GetComponent<Projectile>().launch(this.position, deltaX, deltaY, projectileDamage));
    }
}
