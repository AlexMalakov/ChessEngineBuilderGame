using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAction : HostileEntityAction
{
    public List<HostileEntityAction> actions;

    public ComboAction(HostileEntity opponent, List<HostileEntityAction> actions) : base(opponent) {
        this.actions = actions;
    }

    public override IEnumerator act() {
        foreach (HostileEntityAction action in actions) {
            yield return action.act();
        }
    }
}
