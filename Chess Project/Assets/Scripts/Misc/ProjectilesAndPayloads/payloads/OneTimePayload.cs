using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimePayload : Payload
{
    public override void activate(Square s, bool targetPieces, bool targetHostile, int damage) {
        if((s.hasChessPiece() && targetPieces) || (s.hasHostile() && targetHostile)) {
            s.entity.takeDamage(damage);
        }
    }
}
