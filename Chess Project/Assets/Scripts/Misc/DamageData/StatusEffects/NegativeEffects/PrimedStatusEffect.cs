using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimedStatusEffect : StatusEffect
{
    public int PRIMED_DAMAGE = 3;
    public PrimedStatusEffect() : base ("primed") {}

    public override void onAttatch(Entity target) {
        foreach(StatusEffect s in target.statuses) {
            if(this.statusName == s.statusName) {
                target.takeDamage(PRIMED_DAMAGE);
                return;
            }
        }
        base.onAttatch(target);
    }

}
