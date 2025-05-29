using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyalGuardKingStrategy : KingStance
{
    public override void onEncounterStart() {
        Square s = null;
        foreach(ChessPiece p in this.game.getPieces()) {
            if(p.getPieceType() == PieceType.King) {
                s = p.position;
                break;
            }
        }
        if(s == null) {
            Debug.Log("ERROR COULD NOT FIND KING!");
            return;
        }

        foreach(ChessPiece p in this.game.getPieces()) {
            if(Mathf.Abs(p.position.x - s.x) <= 1 && Mathf.Abs(p.position.y - s.y) <= 1) {
                new OverGuardStatusEffect().onAttatch(p);
            }
        }
    }
    public override void onStrategyEnabled() {}
    public override void onStrategyDisabled() {}

    public override string getRewardName() {
        return "Strategy: Royal Guard";
    }
    public override string getRewardDescription() {
        return "At the start of combat, the king and all adjacent pieces gain an overguard";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "strategy";
    }
}
