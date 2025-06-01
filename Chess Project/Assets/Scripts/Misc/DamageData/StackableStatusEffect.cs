using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackableStatusEffect : StatusEffect
{
    public int stacks;

    public StackableStatusEffect(int stacks, string statusName) : base(statusName) {
        this.stacks = stacks;
    }

    public virtual void addStacks(int stacks) {
        this.stacks += stacks;
        if(this.stacks <= 0) {
            this.onRemove();
        }
    }

    public override void onAttatch(Entity target) {
        foreach(StatusEffect s in target.statuses) {
            if(this.statusName == s.statusName) {
                ((StackableStatusEffect)s).addStacks(this.stacks);
                return;
            }
        }
        base.onAttatch(target);
    }
}
