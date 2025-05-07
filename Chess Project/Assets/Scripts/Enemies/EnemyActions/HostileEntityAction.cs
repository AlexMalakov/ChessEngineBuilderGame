using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileEntityAction : MonoBehaviour
{
    public HostileEntity opponent;

    public abstract IEnumerator act();

    public virtual IEnumerator takeAction() {
        yield return this.act();
        this.opponent.onTurnOver();
    }
}
