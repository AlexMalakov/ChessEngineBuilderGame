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
    public GameObject attackableSprite;
    public GameObject canTargetSprite;

    public ChessPiece piece;
    public Enemy enemy; //this will be updated later ig

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

    public void pieceOnSquare(ChessPiece p) {
        this.piece = p;
    }
    
    public void pieceOffSquare() {
        this.piece = null;
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

    public static bool operator !=(Square a, Square b)
    {
        return !(a == b);
    }

    public void toggleTarget(bool targeting) {
        this.isTargeting = targeting;
        this.canTargetSprite.SetActive(targeting);
    }
}
