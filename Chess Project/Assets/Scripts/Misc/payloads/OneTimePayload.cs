using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimePayload : Payload
{
    public override void activate(Square s, bool targetPieces, bool targetEnemies, bool targetMinions, int damage) {
        s.entity.takeDamage(damage);
    }
}
