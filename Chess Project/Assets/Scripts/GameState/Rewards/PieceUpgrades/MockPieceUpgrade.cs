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

    public override string getRewardName() {
        return "";
    }
    public override string getRewardDescription() {
        return "";
    }
    public override string getRewardFlavorText() {
        return "";
    }
    public override string getRewardImage() {
        return "";
    }
}
