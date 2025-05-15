using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalAction : HostileEntityAction
{
    public HostileEntityAction primaryAction;
    public HostileEntityAction backupAction;

    public override IEnumerator act() {
        if(primaryAction.canAct()) {
            yield return primaryAction.act();
        } else {
            yield return backupAction.act();
        }
    }
}
