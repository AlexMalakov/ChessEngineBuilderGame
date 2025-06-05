using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEnemy : MultiEnemies
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class KnightAttackAction : HostileEntityAction
{

    public KnightAttackAction(KnightEnemy k) : base(k) {}
    public KnightAttackAction(KnightMinion k) : base(k) {}

    public override IEnumerator act() {
        List<Square> squares = new List<Square>();
        List<int[]> moves = new List<int[]>();
        moves.Add(new int[]{2,-1});
        moves.Add(new int[]{2,1});
        moves.Add(new int[]{1,2});
        moves.Add(new int[]{-1,2});
        moves.Add(new int[]{-2,-1});
        moves.Add(new int[]{-2,1});
        moves.Add(new int[]{-1,-2});
        moves.Add(new int[]{1,-2});

        Square s = this.opponent.position;
        
        foreach(int[] m in moves) {
            if(this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]) != null 
                || this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]).hasChessPiece()) {
                
                this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]).entity.takeDamage(this.opponent.damage);
            }
        }

        yield return null;
    }
}

public class KnightCondHopAction : MoveToBestAction
{
    public KnightCondHopAction(KnightEnemy k, bool guarding) : base(k) {
        this.guarding = guarding;
    }
    public KnightCondHopAction(KnightMinion k, bool guarding) : base(k) {
        this.guarding = guarding;
    }
    
    bool guarding;

    public override List<Square> opponentMoves() {
        return this.getSquaresAround(this.opponent.position, false);
    }

    private List<Square> getSquaresAround(Square s, bool canHaveEntity) {
        List<Square> hops = new List<Square>();
        List<int[]> moves = new List<int[]>();
        moves.Add(new int[]{2,-1});
        moves.Add(new int[]{2,1});
        moves.Add(new int[]{1,2});
        moves.Add(new int[]{-1,2});
        moves.Add(new int[]{-2,-1});
        moves.Add(new int[]{-2,1});
        moves.Add(new int[]{-1,-2});
        moves.Add(new int[]{1,-2});

        foreach(int[] move in moves) {
            Square posMove = this.opponent.game.getBoard().getSquareAt(s.x + move[0], s.y + move[1]);
            if(posMove != null && (posMove.entity == null || canHaveEntity)) {
                hops.Add(posMove);
            }
        }
        return hops;
    }

    public override int moveCondition(Square sq) {
        if(guarding) {
            return this.maximizeBoost(this.getSquaresAround(sq, true));
        } else {
            return this.maximizeDamage(this.getSquaresAround(sq, true));
        }
    }
    
}

public class KnightGuardAction : HostileEntityAction
{
    public KnightGuardAction(KnightEnemy k) : base(k) {}
    public KnightGuardAction(KnightMinion k) : base(k) {}
    int guardAmount = 3; 

    public override IEnumerator act() {
        List<Square> squares = new List<Square>();
        List<int[]> moves = new List<int[]>();
        moves.Add(new int[]{2,-1});
        moves.Add(new int[]{2,1});
        moves.Add(new int[]{1,2});
        moves.Add(new int[]{-1,2});
        moves.Add(new int[]{-2,-1});
        moves.Add(new int[]{-2,1});
        moves.Add(new int[]{-1,-2});
        moves.Add(new int[]{1,-2});

        Square s = this.opponent.position;
        
        foreach(int[] m in moves) {
            if(this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]) != null 
                || this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]).hasHostile()) {
                new GuardStatusEffect(guardAmount).onAttatch(this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]).entity);
            }
        }

        yield return null;
    }
}

public class KnightHopAction : HostileEntityAction
{
    public KnightHopAction(KnightEnemy k) : base(k) {}
    public KnightHopAction(KnightMinion k) : base(k) {}

    public override IEnumerator act() {
        List<Square> squares = new List<Square>();
        List<int[]> moves = new List<int[]>();
        moves.Add(new int[]{2,-1});
        moves.Add(new int[]{2,1});
        moves.Add(new int[]{1,2});
        moves.Add(new int[]{-1,2});
        moves.Add(new int[]{-2,-1});
        moves.Add(new int[]{-2,1});
        moves.Add(new int[]{-1,-2});
        moves.Add(new int[]{1,-2});

        Square s = this.opponent.position;
        
        foreach(int[] m in moves) {
            if(this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]) == null 
                || this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]).entity != null) {
                squares.Add(this.opponent.game.getBoard().getSquareAt(s.x + m[0], s.y + m[1]));
            }
        }

        yield return this.opponent.slide(squares[Random.Range(0, squares.Count)]);
    }
}