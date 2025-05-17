using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardStatusEffect : StackableStatusEffect
{
    public GuardStatusEffect(int stacks) : base (stacks, "guard") {}

    public override int affectIncomingDamage(int incomingDamage) {
        int preDamage = incomingDamage;
        incomingDamage = Mathf.Min(0, incomingDamage - this.stacks);
        this.addStacks(-preDamage);
        return incomingDamage;
    }
}
