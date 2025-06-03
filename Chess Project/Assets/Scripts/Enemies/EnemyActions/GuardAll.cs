using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//note: duplicated code between this and buff all, may need to find a way to abstract
public class GuardAll : HostileEntityAction
{
    public bool isEnemy;

    public int guardAmount;

    public override IEnumerator act() {
        if(isEnemy) {
            foreach(Minion m in ((Enemy)this.opponent).minions) {
                if(m.alive) {
                    new GuardStatusEffect(guardAmount).onAttatch(m);
                }
            }
        } else {

            new GuardStatusEffect(guardAmount).onAttatch(((Minion)this.opponent).enemy);
            foreach(Minion m in ((Minion)this.opponent).enemy.minions) {
                if(m == this.opponent) {
                    continue;
                }
                if(m.alive) {
                   new GuardStatusEffect(guardAmount).onAttatch(m);
                }
            }
        }
        this.opponent.game.getEncounter().encReporter.onStatUpdate();
        yield return null;
    }
}
