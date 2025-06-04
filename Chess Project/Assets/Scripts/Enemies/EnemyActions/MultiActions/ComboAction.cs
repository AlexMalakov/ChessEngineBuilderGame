using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAction : HostileEntityAction
{
    public List<HostileEntityAction> actions;

    public override IEnumerator act() {
        foreach (HostileEntityAction action in actions) {
            yield return action.act();
        }
    }
}
