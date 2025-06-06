using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingEnemy : Enemy
{
    public override void onEncounterStart() {
        base.onEncounterStart();
        this.summonMinions();
    }

    public override void initActions() {
        this.actionQueue = new List<HostileEntityAction>();
        this.actionLoop = new List<HostileEntityAction>();

        this.actionLoop.Add(new BoostOthersAction(this, BoostType.status, new string[]{"guard", "5"}));
        this.actionLoop.Add(new ComboAction(this, new List<HostileEntityAction>(){new KingRetreatAction(this), new BoostAllAction(this, BoostType.damage, new string[]{"1"})}));
        this.actionLoop.Add(new SummonWaveAction(this, true));
    }
}

public class KingRetreatAction : Retreat {

    public KingRetreatAction(KingEnemy k) : base(k) {}
    public override List<Square> getRetreatableSquares() {
        List<Square> moves = new List<Square>();
        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{0,-1});
        offsets.Add(new int[]{0,1});
        offsets.Add(new int[]{1,0});
        offsets.Add(new int[]{-1,0});
        offsets.Add(new int[]{-1,-1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{1,1});

        foreach(int[] offset in offsets) {
            if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x + offset[0], this.opponent.position.y + offset[1]) != null
                        && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x + offset[0], this.opponent.position.y + offset[1]).entity == null) {
                
                moves.Add(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x + offset[0], this.opponent.position.y + offset[1]));
            }
        }

        return moves;
    }
}
