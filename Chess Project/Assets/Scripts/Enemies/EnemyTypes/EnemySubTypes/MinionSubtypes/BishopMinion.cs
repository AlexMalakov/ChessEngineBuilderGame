using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopMinion : MultiMinion
{
    public override void initActions() {
        this.actionQueue = new List<HostileEntityAction>();
        this.actionLoop = new List<HostileEntityAction>();
    }
}

public class BishopStabDiags : HostileEntityAction
{
    public BishopStabDiags(BishopMinion b) : base(b) {}
    public override IEnumerator act() {

        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y+1) != null 
            && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y+1).hasChessPiece()) {

            this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y+1).entity.takeDamage(this.opponent.damage);
        }

        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y+1) != null 
            && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y+1).hasChessPiece()) {

            this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y+1).entity.takeDamage(this.opponent.damage);
        }

        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y-1) != null 
            && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y-1).hasChessPiece()) {

            this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y-1).entity.takeDamage(this.opponent.damage);
        }

        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y-1) != null 
            && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y-1).hasChessPiece()) {

            this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y-1).entity.takeDamage(this.opponent.damage);
        }

        yield return null;
    }
}



//this is a combo action of moving to the square, and attacking the 4 corners
public class BishopMoveSlash : MoveToBestAction
{
    List<List<Square>> moveDirections;
    public BishopMoveSlash(BishopMinion b) : base(b) {}

    public override IEnumerator act() {
        Square best = this.findBest();

        if(this.opponent.position.x - best.x == this.opponent.position.y - best.y) {
            yield return base.act();
        } else {
            foreach(List<Square> moveDirection in moveDirections) {
                if(moveDirection.Contains(best)) {
                    yield return this.opponent.slide(moveDirection[0]);
                    yield return this.opponent.slide(best);
                }
            }
            yield return null;
        }
    }

    public override List<Square> opponentMoves() {
        List<Square> moves = new List<Square>();
        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{-1,-1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,1});
        this.moveDirections = new List<List<Square>>();

        foreach(int[] offset in offsets) {
            bool bounced = false;
            List<Square> moveDirection = new List<Square>();
            int x = opponent.position.x; int y = opponent.position.y;
            while(true) {
                Square s = this.opponent.game.getBoard().getSquareAt(x + offset[0], y + offset[1]);
                if(s == null && !bounced) {
                    bounced = true;
                    moveDirection = new List<Square>();
                    if(x + offset[0] >= this.opponent.game.getBoard().len || x + offset[0] < 0) {
                        offset[0] = -offset[0];
                    }
                    if(y + offset[1] >= this.opponent.game.getBoard().height || y + offset[1] < 0) {
                        offset[1] = -offset[1];
                    }
                    continue;
                } else if(s == null || s.entity != null) {
                    break;
                }

                moves.Add(s);
                moveDirection.Add(s);
                x += offset[0]; y += offset[1];
            }
            moveDirections.Add(moveDirection);
        }

        return moves;
    }

    public override int moveCondition(Square sq) {
        List<Square> canAttack = new List<Square>();
        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x + 1, this.opponent.position.y + 1) != null) {
            canAttack.Add(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x + 1, this.opponent.position.y + 1));
        }
        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x - 1, this.opponent.position.y + 1) != null) {
            canAttack.Add(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x - 1, this.opponent.position.y + 1));
        }
        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x + 1, this.opponent.position.y - 1) != null) {
            canAttack.Add(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x + 1, this.opponent.position.y - 1));
        }
        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x - 1, this.opponent.position.y - 1) != null) {
            canAttack.Add(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x - 1, this.opponent.position.y - 1));
        }
        return this.maximizeDamage(canAttack);
    }
}
