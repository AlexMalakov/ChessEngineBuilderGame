using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMinion : Minion
{
    public override void initActions() {
        this.actionQueue = new List<HostileEntityAction>();
        this.actionLoop = new List<HostileEntityAction>();
    }
}
