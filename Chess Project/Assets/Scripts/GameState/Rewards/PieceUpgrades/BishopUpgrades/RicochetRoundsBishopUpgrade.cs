using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetRoundsBishopUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getMoves);
        return changes;
    }
    
    public override PieceType getPieceTarget() {
        return PieceType.ChessPiece;
    }

    public override List<Square> changePossibleMoves(ChessPiece p, bool all, bool attacking) {
        // if(attacking) {
        //     List<Square> additionalMoves = new List<Square>();

        //     List<int[]> offsets = new List<int[]>();
        //     offsets.Add(new int[]{-1,-1});
        //     offsets.Add(new int[]{1,-1});
        //     offsets.Add(new int[]{-1,1});
        //     offsets.Add(new int[]{1,1});

        //     foreach (int[] offset in offsets) {
        //         bool bounced = false;
        //         int distance = 1;
        //         Square s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
        //         while(true) {
        //             if(s == null && !bounced) {
        //                 bounced = true;
        //             } else if(s == null) {
        //                 break;
        //             }

        //             if(s.entity != null) {
        //                 if(s.entity.getEntityType() == EntityType.Piece) {
        //                     break;
        //                 }
        //                 possibleMoves.Add(s);
        //                 break;
        //             }
        //             possibleMoves.Add(s);
        //             distance++;
        //             s = this.game.getBoard().getSquareAt(this.position.x + offset[0]*distance, this.position.y + offset[1]*distance);
        //         }
        //     }
        //     return possibleMoves;
        // }

        // return additionalMoves;
        return null;
    }

    private bool checkSquare(int x, int y, bool all) {
        return this.game.getBoard().getSquareAt(x, y) != null && ((this.game.getBoard().getSquareAt(x, y).entity == null) || (all && this.game.getBoard().getSquareAt(x, y).entity.getEntityType() == EntityType.Piece));
    }
}
