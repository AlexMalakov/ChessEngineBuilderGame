using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnStatusEffect : StackableStatusEffect
{
    private int newStacks;
    public BurnStatusEffect(int stacks) : base (stacks, "burn") {
        this.newStacks = stacks
    }

    public virtual void addStacks(int stacks) {
        this.newStacks += stacks;
        base.addStacks(stacks);
    }

    public virtual void onRoundEnd() {
        this.target.takeDamage(this.stacks);
        this.stacks = Math.Max(0, this.newStacks); //in case we remove more stacks then we add
        this.newStacks = 0;
    }
}
