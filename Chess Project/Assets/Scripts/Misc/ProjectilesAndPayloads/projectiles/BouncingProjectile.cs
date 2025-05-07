using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingProjectile : Projectile
{
    public int bounces;
    public override bool releasePayload(Square s, bool bounds, int damage) {
        if(bounds) {
            bounces--;
            if(s.x + deltaX < 0 || s.x + deltaX >= this.game.getBoard().len) {
                deltaX = - deltaX;
            } else {
                deltaY = - deltaY;
            }
            return bounces < 0;
        }
        return true;
    }

}
