using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bug fix: watch out for 3 or more bishops
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
        // List<ChessPiece> bishops = new List<ChessPiece>();
        // foreach(ChessPiece piece in this.game.getPieces()) {
        //     if(piece.getPieceType == PieceType.Bishop && p.position != piece.position) {
        //         bishops.Add(piece);
        //     }
        // }

        // foreach(ChessPiece bishop in this.bishops) {

        // }
        return new Operation(OperationTypes.Ignore, 0);

    }
}
