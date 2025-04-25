using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{
    public override List<Square> getPossibleMoves() {
        List<Square> possibleMoves = new List<Square>();
        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{-1,-1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,1});
        offsets.Add(new int[]{0,-1});
        offsets.Add(new int[]{0,1});
        offsets.Add(new int[]{-1,0});
        offsets.Add(new int[]{1,0});

        foreach (int[] offset in offsets) {
            int distance = 1;
            Square s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            while(s != null) {
                if(s.entity != null) {
                    if(s.entity.getEntityType() == EntityType.Piece) {
                        break;
                    }
                    possibleMoves.Add(s);
                    break;
                }
                possibleMoves.Add(s);
                distance++;
                s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            }
        }
        return possibleMoves;
    }

    public override List<Square> getAllMoves() {
        List<Square> possibleMoves = new List<Square>();
        List<int[]> offsets = new List<int[]>();
        offsets.Add(new int[]{-1,-1});
        offsets.Add(new int[]{1,-1});
        offsets.Add(new int[]{-1,1});
        offsets.Add(new int[]{1,1});
        offsets.Add(new int[]{0,-1});
        offsets.Add(new int[]{0,1});
        offsets.Add(new int[]{-1,0});
        offsets.Add(new int[]{1,0});

        foreach (int[] offset in offsets) {
            int distance = 1;
            Square s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            while(s != null) {
                possibleMoves.Add(s);        
                if(s.entity != null) {
                    break;
                }
                distance++;
                s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
            }
        }
        return possibleMoves;
    }
}
