using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopEnemy : MultiEnemies
{
    public override void initActions() {
        this.actionQueue = new List<HostileEntityAction>();
        this.actionLoop = new List<HostileEntityAction>();

        this.actionQueue.Add(new SleepAction(this));

        this.actionLoop.Add(new BishopQuadDiagBlast(this));
        this.actionLoop.Add(new BishopDiagBombard(this));
        this.actionLoop.Add(new ComboAction(this, new List<HostileEntityAction>(){new BishopRetreatAction(this), new BoostAllAction(this, BoostType.heal, new string[]{"10"})}));
    }
}


public class BishopDiagBombard : HostileEntityAction
{
    float timeBetweenShots = .5f;

    int bestKills; int bestDamage; int bestHits;
    int kills; int damageDealt; int hits;
    //launches an attack across every diagonal perpendicular to the direction it travels

    public BishopDiagBombard(BishopEnemy b) : base(b) {}

    public override IEnumerator act() {
        resetCounters();
        int[] bombardInfo = choseBombardDirection();

        this.opponent.launchProjectile(0, bombardInfo[2], bombardInfo[3], this.opponent.damage);
        yield return new WaitForSeconds(timeBetweenShots);

        while(true) {
            if(!moveableSquare(this.opponent.position, bombardInfo[0], bombardInfo[1])) {
                break;
            }
            yield return this.opponent.slide(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x + bombardInfo[0], this.opponent.position.y + bombardInfo[1]));
            this.opponent.launchProjectile(0, bombardInfo[2], bombardInfo[3], this.opponent.damage);
            yield return new WaitForSeconds(timeBetweenShots);
        }

        yield return null;
    }

    //returns deltaX, deltaY, projDeltaX, projDeltaY
    private int[] choseBombardDirection() {
        int[] bests = new int[]{1, 1, 1, -1};
        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{1,1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{-1,-1});
        
        foreach(int[] offset in offsets) {
            for(int i = 0; i <= 1; i++) {
                Square s = this.opponent.position;
                kills = 0; damageDealt = 0; hits = 0;
                this.testDirection(s, (i == 0) ? -offset[0] : offset[0], (i == 0) ? offset[1] : -offset[1]);
                while(true) {
                    if(!moveableSquare(this.opponent.game.getBoard().getSquareAt(s.x, s.y), offset[0], offset[1])) {
                        if(kills > bestKills 
                            || (kills == bestKills && damageDealt > bestDamage)
                            || (kills == bestKills && damageDealt == bestDamage && hits > bestHits)
                            || (kills == bestKills && damageDealt == bestDamage && hits == bestHits && Random.Range(0,2) == 0)) {
                            bests[0] = offset[0];
                            bests[1] = offset[1];
                            bests[2] = (i == 0) ? -offset[0] : offset[0];
                            bests[3] = (i == 0) ? offset[1] : -offset[1];
                        }
                        break;
                    }

                    s = this.opponent.game.getBoard().getSquareAt(s.x + offset[0], s.y + offset[1]);
                    this.testDirection(s, (i == 0) ? -offset[0] : offset[0], (i == 0) ? offset[1] : -offset[1]);
                }
            }
        }

        return bests;
    }

    //returns 3, 2, 1, or 0
    private void testDirection(Square s, int deltaX, int deltaY) {
        while(true) {
            if(this.opponent.game.getBoard().getSquareAt(s.x + deltaX, s.y + deltaY) == null 
                || this.opponent.game.getBoard().getSquareAt(s.x + deltaX, s.y + deltaY).hasHostile()) {
                return;
            }

            s = this.opponent.game.getBoard().getSquareAt(s.x + deltaX, s.y + deltaY);

            if(this.opponent.game.getBoard().getSquareAt(s.x, s.y).hasChessPiece()) {
                Entity e = this.opponent.game.getBoard().getSquareAt(s.x, s.y).entity;
                if(e.health + ((ChessPiece)e).effectiveDefense <= this.opponent.damage) {
                    kills++;
                }
                if(((ChessPiece)e).effectiveDefense < this.opponent.damage) {
                    damageDealt += this.opponent.damage - ((ChessPiece)e).effectiveDefense;
                }
                hits++;
                return;
            }
        }
    }

    private void resetCounters() {
        this.bestKills = 0;
        this.bestDamage = 0;
        this.bestHits = 0;
        this.kills = 0;
        this.damageDealt = 0;
        this.hits = 0;
    }

    private bool moveableSquare(Square s, int deltaX, int deltaY) {
        return this.opponent.game.getBoard().getSquareAt(s.x + deltaX, s.y + deltaY) != null 
            && this.opponent.game.getBoard().getSquareAt(s.x + deltaX, s.y + deltaY).entity == null;
    }
}


public class BishopQuadDiagBlast : HostileEntityAction
{
    public BishopQuadDiagBlast(BishopEnemy b) : base(b) {}
    //launches an attack across every row
    public override IEnumerator act() {
        
        this.opponent.launchProjectile(1, -1, -1, this.opponent.damage);
        this.opponent.launchProjectile(1, -1, 1, this.opponent.damage);
        this.opponent.launchProjectile(1, 1, -1, this.opponent.damage);
        this.opponent.launchProjectile(1, 1, 1, this.opponent.damage);
        yield return null;
    }
}

public class BishopRetreatAction : Retreat {

    public BishopRetreatAction(BishopEnemy b) : base(b) {}
    public BishopRetreatAction(BishopMinion b) : base(b) {}

    public override List<Square> getRetreatableSquares() {
        List<Square> moves = new List<Square>();
        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{1,1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{-1,-1});

        foreach(int[] offset in offsets) {
            Square s = this.opponent.position;
            while(true) {
                if(this.opponent.game.getBoard().getSquareAt(s.x + offset[0], s.y + offset[1]) == null
                    || this.opponent.game.getBoard().getSquareAt(s.x + offset[0], s.y + offset[1]).entity != null) {
                        break;
                }
                s = this.opponent.game.getBoard().getSquareAt(s.x + offset[0], s.y + offset[1]);
                moves.Add(this.opponent.game.getBoard().getSquareAt(s.x + offset[0], s.y + offset[1]));
            }
        }
        return moves;
    }
}
