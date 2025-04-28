using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this is basicallty just a template lol it doens't do anything
public class MockPieceUpgrade : PieceUpgradeReward
{
    public override List<PieceMethods> getAffectedMethods() {
        List<PieceMethods> changes = new List<PieceMethods>();
        return changes;
    }
    
    public override PieceType getPieceTarget() {
        return PieceType.ChessPiece;
    }
}
