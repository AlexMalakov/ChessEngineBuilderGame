using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagRetreat : HostileEntityAction
{
    public int weighExtraAmount;
    public int weighExtraCol;
    public override IEnumerator act() {
        List<Square> retreatableLocations = new List<Square>();

        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{1,1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{-1,-1});

        foreach(int[] offset in offsets) {

            Square s = this.opponent.position;
            while(true) {
                s = this.opponent.game.getBoard().getSquareAt(s.x + offset[0], s.y + offset[1]);
                if(s == null || s.entity != null) {
                    break;
                } 
                retreatableLocations.Add(s);
                if(s.y >= weighExtraCol) {
                    for(int i = 0; i < weighExtraAmount; i++) {
                        retreatableLocations.Add(s);
                    }
                }
            }
        }

        yield return this.opponent.slide(retreatableLocations[Random.Range(0, retreatableLocations.Count)]);
    }
}
