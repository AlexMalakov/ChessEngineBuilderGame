using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : HostileEntityAction
{
    public Retreat(HostileEntity opponent) : base(opponent) {}
    public override IEnumerator act() {
        yield return this.opponent.slide(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x, this.opponent.game.getBoard().height-1));
    }
}
