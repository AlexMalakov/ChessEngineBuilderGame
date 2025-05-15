using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadDiagBlast : HostileEntityAction
{
    public float timeBetweenShot;
    //launches an attack across every row
    public override IEnumerator act() {
        
        this.opponent.launchProjectile(1, -1, -1, this.opponent.damage);
        this.opponent.launchProjectile(1, -1, 1, this.opponent.damage);
        this.opponent.launchProjectile(1, 1, -1, this.opponent.damage);
        this.opponent.launchProjectile(1, 1, 1, this.opponent.damage);
        yield return null;
    }
}
