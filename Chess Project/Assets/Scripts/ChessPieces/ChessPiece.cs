using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : Entity
{
    int effectiveDefense;

    public abstract List<Square> getPossibleMoves();

    //also includes defensive moves :)
    public abstract List<Square> getAllMoves();
    public Sprite activeSprite;


    public override IEnumerator slide(Square moveTo) {
        yield return this.game.getBoard().slideObj(this.gameObject, moveTo);
    }

    //returns true if succesful and false if not
    public virtual bool move(Square square) {
        foreach (Square s in getPossibleMoves()) {
            if (s.x == square.x && s.y == square.y) {
                this.position.entity = null;
                this.position = square;
                this.position.entity = this;
                StartCoroutine(this.slide(square));
                return true;
            }
        }

        return false;
    }

    public void assignEffectiveDefense(int defense) {
        this.effectiveDefense = defense;
    }

    //WILL MOVE A PIECE, do not use unless nessisary
    public virtual void forceMove(Square square) {
        this.position.entity = null;
        this.position = square;
        this.position.entity = this;
        StartCoroutine(this.slide(square));
    }

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

    public override void onDeath() {
        this.game.getBoard().onPieceTaken(this);
    }

    public virtual int getPieceDamage() {
        return this.game.getPlayer().getPieceDamage(this.damage);
    }

    public override EntityType getEntityType() {
        return EntityType.Piece;
    }

    public void onSacrifice() {
        
    }
}
