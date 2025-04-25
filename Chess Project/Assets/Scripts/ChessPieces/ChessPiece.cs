using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int damage;
    public int defense;
    public Square position;
    public int startingX;
    public int startingY;
    public Game game;
    int effectiveDefense;

    public abstract List<Square> getPossibleMoves();

    //also includes defensive moves :)
    public abstract List<Square> getAllMoves();
    public Sprite activeSprite;

    public void place(Transform moveTo) {
        this.transform.position = moveTo.position;
    }

    public void slide(Square moveTo) {
        StartCoroutine(this.game.getBoard().slideObj(this.gameObject, moveTo));
    }

    //returns true if succesful and false if not
    public virtual bool move(Square square) {
        foreach (Square s in getPossibleMoves()) {
            if (s.x == square.x && s.y == square.y) {
                this.position.piece = null;
                this.position = square;
                this.position.piece = this;
                this.slide(square);
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
        this.position.piece = null;
        this.position = square;
        this.position.piece = this;
        this.slide(square);
    }

    public void takeDamage(int damage) {
        // int extraDefense = this.game.getBoard().calcualteDefense(this.position);
        if(effectiveDefense > damage) {
            effectiveDefense-=damage;
        } else {
            this.health = this.health + effectiveDefense - damage;
            effectiveDefense = 0;

        }

        if(this.health <= 0) {
            this.game.getBoard().onPieceTaken(this);
        }
    }

    public virtual int getPieceDamage() {
        return this.game.getPlayer().getPieceDamage(this.damage);
    }
}
