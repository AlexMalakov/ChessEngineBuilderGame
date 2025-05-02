using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartyrdomBishopUpgrade : PieceUpgradeReward
{
    private bool oddSquareSac = 0;
    private bool evenSquareSac = 0;
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        changes.Add(PieceMethods.onSacrifice);
        changes.Add(PieceMethods.getPieceDamage);
        return changes;
    }
    
    public override PieceType getPieceTarget() {
        return PieceType.ChessPiece;
    }

    public override void changeOnSacrifice(ChessPiece p){
        if(p.getPieceType() == PieceType.Bishop) {

            if(oddSquareSac == 0 && evenSquareSac == 0) {
                addRoundOverListener(this);
            }

            if((p.position.x + p.position.y) % 2 == 0) {
                evenSquareSac += 100;
            } else {
                oddSquareSac += 100;
            }
        }
    }

    public override Operation changePieceDamage(ChessPiece p, Square target){
        if((p.position.x + p.position.y) % 2 == 0) {
            return new Operation(OperationTypes.Multiply, evenSquareSac);
        } else {
            return new Operation(OperationTypes.Multiply, oddSquareSac);
        }
        return new Operation(OperationTypes.Ignore, 0);
    }

    public override void notifyRoundOver() {
        this.oddSquareSac = 0; this.evenSquareSac = 0;
        this.game.removeRoundOverListener(this);
    }
}
