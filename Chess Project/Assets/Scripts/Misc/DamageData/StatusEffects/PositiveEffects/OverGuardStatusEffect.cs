using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverGuardStatusEffect : StatusEffect
{

    public OverGuardStatusEffect() : base ("overguard") {}

    public override int affectIncomingDamage(int incomingDamage) {
        this.onRemove();
        return 0;
    }
}
