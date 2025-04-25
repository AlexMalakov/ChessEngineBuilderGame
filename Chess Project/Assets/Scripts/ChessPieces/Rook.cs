using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public bool canCastle = true;
    public override bool move(Square c) {
        if(base.move(c)) {
            this.canCastle = false;
            return true;
        }
        return false;
    }

    public override List<Square> getPossibleMoves() { //NEED TO ADD CASTLE ABILITY NVM WE GOOD
        List<Square> possibleMoves = new List<Square>();
        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{-1,0});
        offsets.Add(new int[]{1,0});
        offsets.Add(new int[]{0,-1});
        offsets.Add(new int[]{0,1});

        foreach (int[] offset in offsets) {
            int distance = 1;
            Square s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            while(s != null && s.piece == null) {
                possibleMoves.Add(s);        
                if(s.enemy != null) {
                    break;
                }
                distance++;
                s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            }
        }
        return possibleMoves;
    }

        public override List<Square> getAllMoves() {
        List<Square> possibleMoves = new List<Square>();
        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{-1,0});
        offsets.Add(new int[]{1,0});
        offsets.Add(new int[]{0,-1});
        offsets.Add(new int[]{0,1});

        foreach (int[] offset in offsets) {
            int distance = 1;
            Square s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            while(s != null) {
                possibleMoves.Add(s);        
                if(s.enemy != null || s.piece != null) {
                    break;
                }
                distance++;
                s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            }
        }
        return possibleMoves;
    }
}
