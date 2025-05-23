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
        return PieceType.Bishop;
    }

    public override List<Square> changePossibleMoves(ChessPiece p, bool defending, bool attacking) {
        List<Square> additionalMoves = new List<Square>();
        if(attacking) {
            List<int[]> offsets = new List<int[]>();
            offsets.Add(new int[]{-1,-1});
            offsets.Add(new int[]{1,-1});
            offsets.Add(new int[]{-1,1});
            offsets.Add(new int[]{1,1});

            foreach (int[] offset in offsets) {
                bool bounced = false;
                Square s = p.position;
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

    public override string getRewardName() {
        return "Ricochet Rounds";
    }
    public override string getRewardDescription() {
        return "Bishop's attacks bounce off the edge of the board";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "bishop";
    }
}
