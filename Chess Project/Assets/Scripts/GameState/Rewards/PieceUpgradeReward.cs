using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceUpgradeReward : Reward
{
    public override void applyEffect() {
        this.game.getPlayer().upgradePieces(this);
    }

    public abstract void onBind(ChessPiece p);

    public abstract PieceType getPieceTarget();

    //called by a piece when running getAllMoves/getPossibleMoves, returns new squares that should be considered
    //this might need to be tweaked to account for complete move behavior changes
    public abstract List<Square> changePossibleMoves(bool all);
    
    //called when a piece is moving to a square, adds new behavior
    public abstract bool changeMove(Square square);

    //called when a piece is getting it's defense value, changes how its calcualted
    public abstract int changeDefense();

    //called when a piece is forcefully moved (castling, cc???), changes how it responds
    public abstract void changeForceMove(Square square);

    //called when a piece takes damage, changes how it responds??
    public abstract void changeTakeDamage(int damage);

    //called when a piece dies, adds a new behavior
    public abstract void changeOnDeath();

    //changes how a piece's damage is calculated
    public abstract int changePieceDamage();

    //adds new sacrifice behavior/damage???
    public abstract void changeOnSacrifice();

}