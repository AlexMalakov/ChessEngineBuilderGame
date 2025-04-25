using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAction : EnemyAction
{
    public int powerGain;

    public override IEnumerator act() {
        this.enemy.damage += powerGain;
        this.enemy.reporter.onStatUpdate();
        yield return null;
    }
}
