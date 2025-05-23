using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HinderStatusEffect : StatusEffect //rollWithLuck
{
    public HinderStatusEffect() : base ("hinder") {}

    public override int affectOutgoingDamage(int damage) {
        return (this.target.game.getPlayer().rollWithLuck(.3f, this.target.position.hasChessPiece())) ? 0 : damage;
    }

    public override bool affectMoveAttempt(Square destination) {
        return (this.target.game.getPlayer().rollWithLuck(.3f, this.target.position.hasChessPiece())) ? false : true;
    }

    public override int affectOutgoingDefense(int defense) {
        return (this.target.game.getPlayer().rollWithLuck(.3f, this.target.position.hasChessPiece())) ? 0 : defense;
    }

    public override void onRoundEnd() {
        this.onRemove();
    }
}
