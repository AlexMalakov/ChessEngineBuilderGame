using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KingStance : PieceUpgradeReward
{
    protected bool active = false;

    //this should be overriden for stances that do actually affect methods
    public override List<PieceMethods> getAffectedMethods() {
        return new List<PieceMethods>();
    }

    //this should be overriden for stances that do actually affect other pieces
    public override PieceType getPieceTarget() {
        return PieceType.King;
    }

    //enables or disables this stance
    public void setActive(bool activeState) {
        if(!this.canSwap()) {
            Debug.Log("CANNOT SWAP STRATEGY!");
            return;
        }

        this.active = activeState;
        if(this.active) {
            this.onStrategyEnabled();
        } else {
            this.onStrategyDisabled();
        }
    }

    //if a stance for some reason prevents you from swapping off it
    public virtual bool canSwap() {
        return true;
    }

    //if a stance/strategy prevents you from swapping to it
    public virtual bool canSwapTo() {
        return true;
    }

    public abstract void onStrategyEnabled();
    public abstract void onStrategyDisabled();

    //this does not seem to be needed since upgraderewardmethods cover it
    // public abstract void whileActive();
}
