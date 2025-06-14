using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAction : HostileEntityAction
{
    public List<HostileEntityAction> actions;

    public RandomAction(HostileEntity opponent, List<HostileEntityAction> actions) : base(opponent) {
        this.actions = actions;
    }

    public override IEnumerator act() {
        yield return this.actions[Random.Range(0, this.actions.Count)].act();
    }
}
