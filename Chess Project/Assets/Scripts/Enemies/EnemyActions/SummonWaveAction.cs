using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonWaveAction : HostileEntityAction
{
    public bool isWaveSummonOnly;

    public SummonWaveAction(HostileEntity opponent, bool isWaveSummonOnly) : base(opponent) {
        this.isWaveSummonOnly = isWaveSummonOnly;
    }
    
    public override IEnumerator act() {
        if(this.opponent is Enemy) {
            if(isWaveSummonOnly) {
                ((Enemy)this.opponent).summonMinions();
            } else {
                ((Enemy)this.opponent).summonMinionWave();
            }
        }
        yield return null;
    }
}
