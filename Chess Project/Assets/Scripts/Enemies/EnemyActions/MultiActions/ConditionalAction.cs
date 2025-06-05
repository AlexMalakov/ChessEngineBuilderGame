using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalAction : HostileEntityAction
{
    public HostileEntityAction primaryAction;
    public HostileEntityAction backupAction;

    public ConditionalAction(HostileEntity opponent, HostileEntityAction primaryAction, HostileEntityAction backupAction) : base(opponent) {
        this.primaryAction = primaryAction;
        this.backupAction = backupAction;
    }

    public override IEnumerator act() {
        if(primaryAction.canAct()) {
            yield return primaryAction.act();
        } else {
            yield return backupAction.act();
        }
    }
}
