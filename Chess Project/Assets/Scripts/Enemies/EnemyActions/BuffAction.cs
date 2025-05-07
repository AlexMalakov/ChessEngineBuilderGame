using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAction : HostileEntityAction
{
    public int powerGain;

    public override IEnumerator act() {
        this.opponent.damage += powerGain;
        this.opponent.game.getEncounter().encReporter.onStatUpdate();
        yield return null;
    }
}
