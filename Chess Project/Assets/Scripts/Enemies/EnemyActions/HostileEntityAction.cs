using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HostileEntityAction
{
    public HostileEntity opponent;

    public HostileEntityAction(HostileEntity opponent) {
        this.opponent = opponent;
    }

    public abstract IEnumerator act();

    public virtual bool canAct(){return true;}

    public virtual IEnumerator takeAction() {
        yield return this.act();
        this.opponent.onTurnOver();
    }
    
}
