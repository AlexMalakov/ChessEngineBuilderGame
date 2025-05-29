//TO CONTINUE TOMORROW: FIX CASTLIGN
//THEN IMPLEMENT HIGHLATING SQUARES WITH PIECES THAN CAN REACH A SQUARE WHEN IT"S SELECTED
//THEN I THINK START ON A BASIC ENEMY

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public bool canCastle = true;
    public List<Rook> rooks;
    List<KingStance> strategies = new List<KingStance>();
    int activeStrategy = 0;
    public int maxStrategies = 2;

    public override bool move(Square c) {
        if(this.canCastle && Math.Abs(this.position.x - c.x) == 2) {
            foreach(Square s in this.getPossibleMoves(false)) {
                if(s == c) {
                    
                    foreach (Rook r in rooks) {
                        if(c.x > this.position.x && r.position.x > this.position.x) {
                            r.forceMove(this.game.getBoard().getSquareAt(this.position.x + 1, this.position.y));
                        } else if(c.x < this.position.x && r.position.x < this.position.x) {
                            r.forceMove(this.game.getBoard().getSquareAt(this.position.x - 1, this.position.y));
                        }
                    }
                    this.forceMove(c);
                    this.canCastle = false;
                    return true;
                }
            }
        }
        
        if(base.move(c)) {
            this.canCastle = false;
            return true;
        }
        return false;
    }

    public override List<Square> getPossibleMoves(bool attacking) {
        List<Square> possibleMoves = new List<Square>();

        if(this.pieceUpgrades.ContainsKey(PieceMethods.getMoves)) {
            foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.getMoves]) {
                possibleMoves.AddRange(upgrade.changePossibleMoves(this, false, attacking));
            }
        }

        List<int[]> moves = new List<int[]>();
        moves.Add(new int[]{0,-1});
        moves.Add(new int[]{0,1});
        moves.Add(new int[]{1,0});
        moves.Add(new int[]{-1,0});
        moves.Add(new int[]{-1,-1});
        moves.Add(new int[]{-1,1});
        moves.Add(new int[]{1,-1});
        moves.Add(new int[]{1,1});

        foreach (int[] move in moves) {
            Square s = this.game.getBoard().getSquareAt(this.position.x + move[0], this.position.y + move[1]);
            if(s != null && s.entity == null) {
                possibleMoves.Add(s);
            }
        }

        if(canCastle) {
            foreach(Rook r in this.rooks) {
                if(r.canCastle && canCastleWith(r)) {
                    if(r.position.x > this.position.x) {
                        possibleMoves.Add(this.game.getBoard().getSquareAt(this.position.x+2, this.position.y));
                    } else {
                        possibleMoves.Add(this.game.getBoard().getSquareAt(this.position.x-2, this.position.y));
                    }
                }
            }
        }
        

        return possibleMoves;
    }

    public override List<Square> getDefensiveMoves() {
        List<Square> possibleMoves = new List<Square>();

        if(this.pieceUpgrades.ContainsKey(PieceMethods.getMoves)) {
            foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.getMoves]) {
                possibleMoves.AddRange(upgrade.changePossibleMoves(this, true, false));
            }
        }

        List<int[]> moves = new List<int[]>();
        moves.Add(new int[]{0,-1});
        moves.Add(new int[]{0,1});
        moves.Add(new int[]{1,0});
        moves.Add(new int[]{-1,0});
        moves.Add(new int[]{-1,-1});
        moves.Add(new int[]{-1,1});
        moves.Add(new int[]{1,-1});
        moves.Add(new int[]{1,1});

        foreach (int[] move in moves) {
            Square s = this.game.getBoard().getSquareAt(this.position.x + move[0], this.position.y + move[1]);
            if(s != null) {
                possibleMoves.Add(s);
            }
        }

        return possibleMoves;
    }


    private bool canCastleWith(Rook r) {
        for(int i = Math.Min(this.position.x, r.position.x)+1; i < Math.Max(this.position.x, r.position.x); i++) {
            if(this.game.getBoard().getSquareAt(i,this.position.y).entity != null) {
                return false;
            }
        }
        return true;
    }

    public override PieceType getPieceType() {
        return PieceType.King;
    }

    public override void takeDamage(int damage) {
        base.takeDamage(damage);
        this.game.getPlayer().reporter.onPlayerUpdate();
    }


    //strategy/stance stuff:

    public bool canActivateStrategy() {
        return this.strategies.Count > 0 && (this.strategies[this.activeStrategy%this.strategies.Count] is KingStanceWithActive);
    }

    public void activateCurrentStrategy() {
        if(canActivateStrategy()) {
            ((KingStanceWithActive)this.strategies[this.activeStrategy%this.strategies.Count]).onActivate();
        } else {
            Debug.Log("CANNOT ACTIVAT ETHE CURRENT ABILITY");
        }
    }

    public virtual bool canSwapStrategy() {
        return this.strategies.Count > 1 || this.strategies[this.activeStrategy%this.strategies.Count].canSwap();
    }

    public virtual void swapStrategy() {
        this.strategies[this.activeStrategy%this.strategies.Count].setActive(false);
        this.activeStrategy++;
        this.strategies[this.activeStrategy%this.strategies.Count].setActive(true);
    }

    public virtual List<KingStance> getStrategies() {
        return this.strategies
    }

    public virtual void addStrategy(KingStance swappingIn, KingStance swappingOut) {
        if(swappingOut != null) {
            this.strategies.Remove(swappingOut);
            this.strategies.Add(swappingIn);
        } else if(this.strategies.Count < this.maxStrategies) {
            this.strategies.Add(swappingIn);
        } else {
            Debug.Log("CANNOT ADD THE STRATEGY TO THE KING");
        }
    }
}
