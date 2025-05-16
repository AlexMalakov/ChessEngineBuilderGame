using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackableStatusEffect : StatusEffect
{
    int stacks;

    public StackableStatusEffect(string statusName, int stacks) : base(statusName) {
        this.stacks = stacks;
    }

    public virtual void addStacks(int stacks) {
        this.stacks += stacks;
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
