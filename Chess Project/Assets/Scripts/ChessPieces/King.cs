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

    public override bool move(Square c) {
        if(this.canCastle && Math.Abs(this.position.x - c.x) == 2) {
            foreach( Square s in this.getPossibleMoves()) {
                if( s == c) {
                    
                    foreach (Rook r in rooks) {
                        if( c.x > this.position.x && r.position.x > this.position.x) {
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

    public override List<Square> getPossibleMoves() {
        List<Square> possibleMoves = new List<Square>();
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
            if(s != null && s.piece == null) {
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

    public override List<Square> getAllMoves() {
        List<Square> possibleMoves = new List<Square>();
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
            if(this.game.getBoard().getSquareAt(i,this.position.y).piece != null) {
                return false;
            }
        }
        return true;
    }
}
