using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceUpgradeReward : Reward
{
    public int priority; //may not do anything, but could be a good way to determine if 

    public override void applyEffect() {
        this.game.getPlayer().upgradePieces(this);
    }

    //returns every method that this upgrade will modify
    public abstract List<PieceMethods> getAffectedMethods();

    public virtual void onBind(ChessPiece p) {}

    public abstract PieceType getPieceTarget();

    //called by a piece when running getAllMoves/getPossibleMoves, returns new squares that should be considered
    //all is if it includes defensive moves or not :)
    //this might need to be tweaked to account for complete move behavior changes
    public virtual List<Square> changePossibleMoves(ChessPiece p, bool all, bool attacking) {
        return new List<Square>();
    }
    
    //called when a piece is moving to a square, adds new behavior
    //returns true if the move overrides, and false if it doesn't
    public virtual bool changeMove(ChessPiece p, Square square) {
        return true;
    }

    public virtual bool changeAfterMove(ChessPiece p, Square square) {
        return true;
    }

    public virtual Operation changeEffectiveDefense(ChessPiece p, int defense) {
        return new Operation(OperationTypes.Ignore, 0);
    }

    //called when a piece is getting it's defense value, changes how its calcualted
    public virtual Operation changeDefense(ChessPiece p) {
        return new Operation(OperationTypes.Ignore, 0);
    }

    //called when a piece is forcefully moved (castling, cc???), changes how it responds
    //returns true if behavior was changed
    public virtual bool changeForceMove(ChessPiece p, Square square) {
        return false;
    }

    //called when a piece takes damage, changes how it responds??
    public virtual void changeTakeDamage(ChessPiece p, int damage) {}

    //called when a piece dies, adds a new behavior
    public virtual void changeOnDeath(ChessPiece p) {}

    //changes how a piece's damage is calculated
    public virtual Operation changePieceDamage(ChessPiece Piece, Square target){
        return new Operation(OperationTypes.Ignore, 0);
    }

    public virtual IENumerator changeAttack(ChessPiece p, Entity target) {
        return null;
    }

    //adds new sacrifice behavior/damage???
    public virtual void changeOnSacrifice(ChessPiece p){}

    public virtual void notifyRoundOver() {}

    public virtual void changePromote(ChessPiece p) {}

}