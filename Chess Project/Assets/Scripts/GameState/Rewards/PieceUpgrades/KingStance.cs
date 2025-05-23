using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KingStance : PieceUpgradeReward
{
    protected bool active = false;
    public override List<PieceMethods> getAffectedMethods() {
        return new List<PieceMethods>();
    }

    public override PieceType getPieceTarget() {
        return PieceType.King;
    }

    public void setActive(bool activeState) {
        this.active = activeState;
    }

    public abstract void onRoundStart();

    public abstract void whileActive();

    public abstract void onActivate();
}
