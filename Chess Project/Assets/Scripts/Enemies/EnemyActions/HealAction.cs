using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAction : HostileEntityAction
{
    public List<HostileEntity> targetsToHeal;
    public int amount;
    
    public override IEnumerator act() {
        foreach(HostileEntity target in targetsToHeal) {
            target.health = Mathf.Min(target.maxHealth, target.health + amount);
        }
        yield return null;
    }
}
