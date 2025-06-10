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

    public virtual IEnumerator takeTurn() {
        //grow to indicate it's doing it's action
        float elapsed = 0f;
        Vector3 initialScale = this.transform.localScale;
        Vector3 targetScale = initialScale * this.game.sizeIncrease;
        while(elapsed < this.game.pieceAttackGrowDuration) {
            this.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / this.game.pieceAttackGrowDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if(currentAct < actionQueue.Count) {
            yield return actionQueue[currentAct].takeAction();
        } else {
            yield return actionLoop[(currentAct - actionQueue.Count)%actionLoop.Count].takeAction();
        }
        currentAct++;

        //shrink back to original scale
        elapsed = 0f;
        while(elapsed < this.game.pieceAttackGrowDuration) {
            this.transform.localScale = Vector3.Lerp(targetScale, initialScale, elapsed / this.game.pieceAttackGrowDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
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

    public virtual IEnumerator meleeAnimation(Entity target) {

        float elapsed = 0f;
        Vector3 start = transform.position;
        while(elapsed < this.game.playerAttackDuration) {
            transform.position = Vector3.Lerp(start, target.position.transform.position, elapsed/this.game.playerAttackDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;
        start = transform.position;
        while(elapsed < this.game.playerAttackDuration) {
            transform.position = Vector3.Lerp(start, this.position.transform.position, elapsed/this.game.playerAttackDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
