using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenStatusEffect : StatusEffect
{
    public FrozenStatusEffect() : base ("frozen") {}

    public override int affectOutgoingDamage(int damage) {
        return 0;
    }

    public virtual bool affectMoveAttempt(Square destination) {
        return false;
    }

    public override int affectOutgoingDefense(int defense) {
        return 0;
    }

    public override void onRoundEnd() {
        this.onRemove();
    }
}
