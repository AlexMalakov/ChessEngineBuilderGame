using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartyrdomBishopUpgrade : PieceUpgradeReward
{
    private int oddSquareSac = 0;
    private int evenSquareSac = 0;
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
                this.game.addRoundOverListener(this);
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
    }

    public override void notifyRoundOver() {
        this.oddSquareSac = 0; this.evenSquareSac = 0;
        this.game.removeRoundOverListener(this);
    }

    public override void onEncounterStart() {
        if(this.oddSquareSac > 0 || this.evenSquareSac > 0) {
            this.notifyRoundOver();
        }
    }

    public override string getRewardName() {
        return "Martyrdom";
    }
    public override string getRewardDescription() {
        return "After a bishop is sacrificed, grant all pieces on it's diagonal 100% increased damage";
    }
    public override string getRewardFlavorText() {
        return "placeholder";
    }
    public override string getRewardImage() {
        return "bishop";
    }
}
