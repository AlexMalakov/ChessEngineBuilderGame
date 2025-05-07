using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public int x; public int y;
    public Board parent;
    public Game game;
    public SquareReporter reporter;

    [Header ("target sprites")]
    public GameObject selectedSprite;
    public GameObject moveableSprite;
    public GameObject canTargetSprite;
    public GameObject assistingSprite;

    public Entity entity;

    public bool isMoveable;
    public bool isTargeting;
    public string squareName;

    public void init(int x, int y, Board parent, string name) {
        this.x = x;
        this.y = y;
        this.parent = parent;
        this.squareName = name;
    }

    public void OnMouseDown()
    {
        reporter.onSquareUpdate(this);
        if(game.playersTurn) {
            selectedSprite.SetActive(true);
            this.parent.onSquarePress(this, false); 
        } else if(game.playerPremoveTurn) {
            selectedSprite.SetActive(true);
            this.parent.onSquarePress(this, true);
        }
    }

    public void unclickSquare() {
        selectedSprite.SetActive(false);
    }

    public void setMoveable(bool moveable) {
        this.isMoveable = moveable;
        this.moveableSprite.SetActive(moveable);
    }

    public static bool operator ==(Square a, Square b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;

        return a.x == b.x && a.y == b.y;
    }

    //these are uselss but im getting a warning and it's bothering me >:(
    public override int GetHashCode(){return 0;} //zero reason to use this
    public override bool Equals(object obj){return false;} //don't use this tbh

    public static bool operator !=(Square a, Square b)
    {
        return !(a == b);
    }

    public void toggleTarget(bool targeting) {
        this.isTargeting = targeting;
        this.canTargetSprite.SetActive(targeting);
    }

    public void isAssisting(bool assisting) {
        if(assisting) {
        }
        this.assistingSprite.SetActive(assisting);
    }

    public bool hasChessPiece() {
        return this.entity != null && this.entity.getEntityType() == EntityType.Piece;
    }

    public bool canDamageSquare(int damage) {
        return this.hasChessPiece() && (((ChessPiece)this.entity).effectiveDefense < damage);
    }

    public bool canKillSquare(int damage) {
        return this.hasChessPiece() && (((ChessPiece)this.entity).effectiveDefense + this.entity.health <= damage);
    }

    public bool hasPiece(PieceType pieceType) {
        return this.hasChessPiece() && ((ChessPiece)this.entity).getPieceType() == pieceType;
    }

    public bool hasOpponent() {
        return this.entity != null && (this.entity.getEntityType() == EntityType.Minion || this.entity.getEntityType() == EntityType.Enemy);
    }
}
