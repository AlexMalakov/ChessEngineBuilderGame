using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAction : MonoBehaviour
{
    public Enemy enemy;

    public abstract IEnumerator act();

    public virtual IEnumerator takeAction() {
        yield return this.act();
        this.enemy.onTurnOver();
    }
}
