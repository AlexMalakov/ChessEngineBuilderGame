using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAction : EnemyAction
{
    public List<EnemyAction> actions;

    public override IEnumerator act() {
        foreach (EnemyAction action in actions) {
            yield return action.act();
        }
    }
}
