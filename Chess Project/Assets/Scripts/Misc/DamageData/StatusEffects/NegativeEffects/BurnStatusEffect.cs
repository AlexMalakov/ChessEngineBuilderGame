using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnStatusEffect : StackableStatusEffect
{
    private int newStacks;
    public BurnStatusEffect(int stacks) : base (stacks, "burn") {
        this.newStacks = stacks;
    }

    public override void addStacks(int stacks) {
        this.newStacks += stacks;
        base.addStacks(stacks);
    }

    public override void onRoundEnd() {
        this.target.takeDamage(this.stacks);
        this.addStacks(-(this.stacks - Mathf.Max(this.newStacks, 0)));
        this.newStacks = 0;
    }
}
