using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMultiple : HostileEntityAction
{
    public bool isEnemy;

    public int powerGain;

    public override IEnumerator act() {
        if(isEnemy) {
            foreach(Minion m in ((Enemy)this.opponent).minions) {
                m.damage += powerGain;
            }
        } else {

            ((Minion)this.opponent).enemy.damage += powerGain;
            foreach(Minion m in ((Minion)this.opponent).enemy.minions) {
                m.damage += powerGain;
            }

            this.opponent.damage -= powerGain;
        }
        this.opponent.game.getEncounter().encReporter.onStatUpdate();
        yield return null;
    }


}
