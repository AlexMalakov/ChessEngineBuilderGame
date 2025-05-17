using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergizedStatusEffect : StackableStatusEffect
{
    public EnergizedStatusEffect(int stacks) : base (stacks, "energized") {}

    public override int affectOutgoingDamage(int damage) {
        this.onRemove();
        return this.stacks + damage;
    }
}
