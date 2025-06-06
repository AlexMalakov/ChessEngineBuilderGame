using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepAction : HostileEntityAction
{
    public SleepAction(HostileEntity opponent) : base(opponent){}
    public override IEnumerator act() {
        yield return this.opponent.game.effectManager.displayEffect(this.opponent, "sleep", 1f);
    }
}
