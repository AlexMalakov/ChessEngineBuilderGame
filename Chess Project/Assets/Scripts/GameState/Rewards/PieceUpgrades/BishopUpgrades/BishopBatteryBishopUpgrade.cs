using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopBatteryBishopUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.getPieceDamage);
        return changes;
    }

    public override PieceType getPieceTarget() {
        return PieceType.Bishop;
    }
    
    public override Operation changePieceDamage(ChessPiece p, Square target) {
        List<Square> attackMoves = p.getPossibleMoves(true);

        int found = 0;
        for(found = 0; found < attackMoves.Count(); found++) {
            if(attackMoves[found] == target) {
                break;
            }
        }

        int i; int xOffset; int yOffset;
        for(i = found; i >= 0; i--) {
            if(Math.Abs(attackMoves[i].x - p.position.x) == 1 && Mathf.Abs(attackMoves[i].y - p.position.y) == 1) {
                break;
                xOffset = attackMoves[i].x - p.position.x; yOffset = attackMoves[i].y - p.position.y;
            }

            if(i == 0) {
                Debug.Log("ERROR IN BISHOP BATTERY, SHOULD NOT BE ABLE TO REACH THIS!!!!");
                return new Operation(OperationType.Ignore, 0);
            }
        }


        if(Mathf.Abs(p.position.x - target.x) == 1 && Mathf.Abs(p.position.y - target.y) == 1 && p.position.y > target.position.y) {
            return new Operation(OperationType.Multiply, 100);
        }
        return new Operation(OperationType.Ignore, 0);
    }
}
