using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingPayload : Payload
{
    public float splashDamagePercent;
    public int range;
    public bool manhattanDistance;
    public bool distanceBasedFallOff;
    public override void activate(Square s, bool targetPieces, bool targetHostile, int damage) {
        
        List<int[]> offsets = new List<int[]>();

        for(int i = -this.range; i <= this.range; i++) {
            for(int j = -this.range; j <= this.range; j++) {
                if(manhattanDistance && Mathf.Abs(s.x-i) + Mathf.Abs(s.y-j) > this.range) {
                    continue;
                }
                offsets.Add(new int[]{i,j});
            }
        }

        foreach(int[] offset in offsets) {
            Square target = this.game.getBoard().getSquareAt(s.x+offset[0], s.y+offset[1]);
            if(target != null && ((target.hasChessPiece() && targetPieces) || (target.hasHostile() && targetHostile))) {
                if(target == s) {
                    target.entity.takeDamage(damage);
                    continue;
                }
                int distance = (manhattanDistance) ? Mathf.Abs(s.x+offset[0]) + Mathf.Abs(s.y+offset[1]) : Mathf.Max(Mathf.Abs(s.x+offset[0]), Mathf.Abs(s.y+offset[1]));
                float damageToDeal = (distanceBasedFallOff) ? Mathf.Pow(splashDamagePercent, distance) * damage: splashDamagePercent * damage;
                target.entity.takeDamage((int)damageToDeal);
            }
        }
    }
}
