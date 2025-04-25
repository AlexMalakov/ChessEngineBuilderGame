using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimePayload : Payload
{
    public override void activate(Square s, bool targetPieces, bool targetEnemies, bool targetMinions, int damage) {
        if(s.enemy != null) {
            s.enemy.takeDamage(damage);
        } else if(s.piece != null) {
            s.piece.takeDamage(damage);
        } //else if minion
    }
}
