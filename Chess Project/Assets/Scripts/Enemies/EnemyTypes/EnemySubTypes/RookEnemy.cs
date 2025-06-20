using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookEnemy : Enemy
{
    public override void initActions() {
        this.actionQueue = new List<HostileEntityAction>();
        this.actionLoop = new List<HostileEntityAction>();

        this.actionQueue.Add(new SleepAction(this));

        this.actionLoop.Add(new RookBombardAction(this));
        this.actionLoop.Add(new BoostSelfAction(this, BoostType.damage, new string[]{"1"}));
    }
}

public class RookBombardAction : HostileEntityAction
{
    public RookBombardAction(RookEnemy r) : base(r) {}
    float timeBetweenShot = .5f;
    //launches an attack across every row
    public override IEnumerator act() {
        for(int i = 0; i < this.opponent.game.getBoard().len; i++) {
            yield return this.opponent.slide(this.opponent.game.getBoard().getSquareAt(i, this.opponent.position.y));
            this.opponent.launchProjectile(0, 0, -1, this.opponent.damage);
            yield return new WaitForSeconds(timeBetweenShot);
        }

        int destination = Random.Range(0, this.opponent.game.getBoard().len); //replace with a targeted shot at whatever is the weakest
        yield return this.opponent.slide(this.opponent.game.getBoard().getSquareAt(destination, this.opponent.position.y));
        this.opponent.launchProjectile(0, 0, -1, this.opponent.damage);
        yield return new WaitForSeconds(timeBetweenShot);
    }
}