using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingEnemy : Enemy
{
    public override void onEncounterStart() {
        base.onEncounterStart();
        this.summonMinions();
    }

    public override void initActions() {
        this.actionQueue = new List<HostileEntityAction>();
        this.actionLoop = new List<HostileEntityAction>();
    }
}
