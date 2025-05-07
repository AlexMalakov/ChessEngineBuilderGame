using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepAction : HostileEntityAction
{
    public override IEnumerator act() {
        yield return new WaitForSeconds(1f); //this action does nothing teehee
    }
}
