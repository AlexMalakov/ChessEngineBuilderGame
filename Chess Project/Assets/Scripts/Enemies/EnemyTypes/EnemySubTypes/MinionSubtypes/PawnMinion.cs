using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnMinion : Minion
{
    public override void initActions() {
        this.actionQueue = new List<HostileEntityAction>();
        this.actionLoop = new List<HostileEntityAction>();

        PawnAttackAction attack = new PawnAttackAction(this);
        PawnMoveAction move = new PawnMoveAction(this);
        PawnConditionalMoveAction move50 = new PawnConditionalMoveAction(this);
        PawnGuardAction guard = new PawnGuardAction(this);

        this.actionQueue.Add(new ComboAction(this, new List<HostileEntityAction>(){move, move}));
        this.actionQueue.Add(new ComboAction(this, new List<HostileEntityAction>(){attack, guard, move50, guard, attack}));
        
        this.actionLoop.Add(new ComboAction(this, new List<HostileEntityAction>(){attack, guard, move, guard, attack}));
    }
}

public class PawnAttackAction : HostileEntityAction
{
    public PawnAttackAction(PawnMinion p) : base(p) {}
    public override IEnumerator act() {
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
public class PawnConditionalMoveAction : HostileEntityAction
{
    public PawnConditionalMoveAction(PawnMinion p) : base(p) {}
    
    int promotionDamage = 5;

    public override IEnumerator act() {

        if((this.opponent.position.x + this.opponent.position.y) % 2 == 0) {

            if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x, this.opponent.position.y-1) != null
                && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x, this.opponent.position.y-1).entity == null) {
                
                if(this.opponent.position.y-1 == 0) {
                    foreach(ChessPiece p in this.opponent.game.getPieces()) {
                        p.takeDamage(promotionDamage);
                    }
                }
                yield return this.opponent.slide(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x, this.opponent.position.y-1));
            }
        }
        yield return null;
    }
}

public class PawnGuardAction : HostileEntityAction
{
    public PawnGuardAction(PawnMinion p) : base(p) {}
    public override IEnumerator act() {
        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y-1) != null
                && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y-1).hasHostile()) {
                

            new GuardStatusEffect(this.opponent.defense).onAttatch(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x+1, this.opponent.position.y-1).entity);
        }
        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y-1) != null
            && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y-1).hasHostile()) {
            
            new GuardStatusEffect(this.opponent.defense).onAttatch(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x-1, this.opponent.position.y-1).entity);
        }
        yield return null;
    }
}

public class PawnMoveAction : HostileEntityAction
{
    public PawnMoveAction(PawnMinion p ) : base(p) {}
    int promotionDamage = 5;

    public override IEnumerator act() {
        if(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x, this.opponent.position.y-1) != null
            && this.opponent.game.getBoard().getSquareAt(this.opponent.position.x, this.opponent.position.y-1).entity == null) {
            
            if(this.opponent.position.y-1 == 0) {
                foreach(ChessPiece p in this.opponent.game.getPieces()) {
                    p.takeDamage(promotionDamage);
                }
            }
            yield return this.opponent.slide(this.opponent.game.getBoard().getSquareAt(this.opponent.position.x, this.opponent.position.y-1));
        }
        yield return null;
    }
}
