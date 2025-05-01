using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum PieceType {
    ChessPiece,
    Pawn, 
    Knight,
    Bishop,
    Rook,
    Queen,
    King,
}

public enum PieceMethods {
    getMoves, move, assignEffectiveDefense, getDefense, forceMove, takeDamage, onDeath, getPieceDamage, onSacrifice, castle, promote
}

public abstract class ChessPiece : Entity
{
    public int effectiveDefense;

    //also includes defensive moves :)
    public Sprite activeSprite;

    //maps method name as string to pieceUpgrade
    protected Dictionary<PieceMethods, List<PieceUpgradeReward>> pieceUpgrades; 
    public PopUpManager popupManager;



    //gets all a piece's moves, and all pieces it can move to when "defending"
    public abstract List<Square> getAllMoves();

    //gets all the moves a piece can take including sacrifices
    public abstract List<Square> getPossibleMoves(bool attacking);

    //phisically moves a piece along the board to the square moveTo
    public override IEnumerator slide(Square moveTo) {
        yield return this.game.getBoard().slideObj(this.gameObject, moveTo);
    }

    //executes the logic behind movement of a piece to the square square
    //returns true if succesful and false if not
    public virtual bool move(Square square) {
        foreach (Square s in getPossibleMoves(false)) {
            if (s.x == square.x && s.y == square.y) {
                this.position.entity = null;
                this.position = square;
                this.position.entity = this;
                StartCoroutine(this.slide(square));
                return true;
            }
        }

        throw new Exception("move not in possible moevs :'(");
        // return false;
    }

    //assigns effective defense to a piece; this lasts for a round of enemy attacks
    //in theory this method should be called before the next enemy attacks
    public void assignEffectiveDefense(int defense) {
        this.effectiveDefense = defense;
    }

    //gets a piece's defense
    public virtual int getDefense() {
        return this.defense;
    }

    //moves a piece without checking if a move is possible
    //use for stuff like castleing, or for cc
    //WILL MOVE A PIECE, do not use unless nessisary
    public virtual void forceMove(Square square) {
        this.position.entity = null;
        this.position = square;
        this.position.entity = this;
        StartCoroutine(this.slide(square));
    }

    //causes a piece to take damage
    public override void takeDamage(int damage) {
        // int extraDefense = this.game.getBoard().calcualteDefense(this.position);
        if(effectiveDefense > damage) {
            effectiveDefense-=damage;
        } else {
            damage -= effectiveDefense;
            effectiveDefense = 0;
            base.takeDamage(damage);
        }
    }

    //runs when a piece dies
    public override void onDeath() {
        this.game.getBoard().onPieceTaken(this);
    }

    //gets the amount of damage a piece deals
    //at the moment just checks if it crits by asking the player
    public virtual int getPieceDamage(Square target) {
        NumberMultiplier damageCalculator = new NumberMultiplier(this.damage);
        foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.getPieceDamage]) {
            damageCalculator.addOperation(upgrade.changePieceDamage(this, target));
        }
        return this.game.getPlayer().getPieceDamage(damageCalculator.resolve());
    }

    //gets a piece's entity type, to identify it is a piece
    public override EntityType getEntityType() {
        return EntityType.Piece;
    }

    //runs when a piece moves into an enemy, and dies (will do something cool but nothing at the moment lol)
    public void onSacrifice() {

    }

    //adds a piece upgrade into its list of upgrades, may change
    public virtual void mountPieceUpgrade(PieceUpgradeReward upgrade) {
        foreach(PieceMethods methodName in upgrade.getAffectedMethods()) {
            if(!this.pieceUpgrades.ContainsKey(methodName)) {
                pieceUpgrades[methodName] = new List<PieceUpgradeReward>();
            }
            this.pieceUpgrades[methodName].Add(upgrade);
        }
        upgrade.onBind(this);
    }

    //gets a piece's piece type to identify what type of piece it is without checking the class which is annoyting i think
    public abstract PieceType getPieceType();

    public void popUpAction(PopupType type, int value) {
        this.popupManager.displayPopUp(type, value)
    }
}
