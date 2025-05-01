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
        List<Square> additionalMoves = new List<Square>();
        if(attacking) {
            List<int[]> offsets = new List<int[]>();
            offsets.Add(new int[]{-1,-1});
            offsets.Add(new int[]{1,-1});
            offsets.Add(new int[]{-1,1});
            offsets.Add(new int[]{1,1});

            Square s = p.position;
            foreach (int[] offset in offsets) {
                bool bounced = false;
                Square s;
                while(true) {
                    if(this.game.getBoard().getSquareAt(s.x + offset[0], s.y + offset[1]) == null && !bounced) {
                        bounced = true;
                        if(s.x + offset[0] < 0 || s.x + offset[0] >= this.game.getBoard().len) {
                            offset[0] = -offset[0];
                        } else {
                            offset[1] = -offset[1];
                        }
                        continue;
                    } else if(s == null) {
                        break;
                    }
                    s = this.game.getBoard().getSquareAt(s.x + offset[0], s.y + offset[1]);

                    if(s.entity != null) {
                        if(s.hasChessPiece()) {
                            break;
                        }

                        if(bounced) {
                            additionalMoves.Add(s);
                        }
                        break;
                    }

                    if(bounced) {
                        additionalMoves.Add(s);
                    }
                }
            }
            return additionalMoves;
        }

        return additionalMoves;
    }

    private bool checkSquare(int x, int y, bool all) {
        return this.game.getBoard().getSquareAt(x, y) != null && ((this.game.getBoard().getSquareAt(x, y).entity == null) || (all && this.game.getBoard().getSquareAt(x, y).entity.getEntityType() == EntityType.Piece));
    }
}
