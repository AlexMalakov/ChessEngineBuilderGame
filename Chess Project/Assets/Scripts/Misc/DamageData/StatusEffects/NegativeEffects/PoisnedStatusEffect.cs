using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisnedStatusEffect : StatusEffect
{
    public PoisnedStatusEffect() : base ("poisoned") {}


    public override void onRoundEnd() {
        this.target.health -= 1;
        this.target.takeDamage(0); //just to make sure it dies
    }
}
