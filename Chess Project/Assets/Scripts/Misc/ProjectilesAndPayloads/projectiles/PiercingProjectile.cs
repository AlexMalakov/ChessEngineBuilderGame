using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingProjectile : Projectile
{
    public bool piercePieces;
    public bool pierceHostile;
    public int pierces;
    public float pierceDamageReduction;

    bool hasPiereced = false;

    public override bool releasePayload(Square s, bool bound, int damage) {
        if(bound || pierces == 0) {
            return true;
        }

        float damageToDeal = hasPiereced ? pierceDamageReduction * damage : damage * 1.0f;

        if((s.hasChessPiece() && piercePieces) || (s.hasHostile() && pierceHostile)) {

            if((this.targetPieces && s.hasChessPiece()) || (this.targetHostile && s.hasHostile())) {
                this.payload.activate(s, this.targetPieces, this.targetHostile, (int)damageToDeal);
            }

            pierces--;
            hasPiereced = true;
            return false;
        } 
        return true;
    }
}
