using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header ("stats")]
    public int agility; //how many moves you can make between an opponents turn
    public int perception; //knowledge of the opponents future actions + premoves
    public int luck; //adds crit damage to shots, and improves percentage effects and rewards
    public int endurance; //heals pieces after each round by this amount, increases piece max health?
    public int blockStatIllFigureItOutLater; //something that helps the block build
    // punlic int ;
    //other stats will be added later
    // public int 


    float critDamage = .2f; //placeholder, we can do stuff with this probabvly

    [Header ("pieces")]
    public List<ChessPiece> livingPieces;
    public List<ChessPiece> deadPieces;
    public List<ChessPiece> premovingPieces;
    public List<ChessPiece> nonPremovingPieces;


    public void onTurnStart() {
        premovingPieces = new List<ChessPiece>();
        nonPremovingPieces = new List<ChessPiece>(livingPieces);
    }

    public void onPieceDeath(ChessPiece p) {
        livingPieces.Remove(p);
        premovingPieces.Remove(p);
        nonPremovingPieces.Remove(p);
        deadPieces.Add(p);
    }

    public void premovePiece(ChessPiece p) {
        premovingPieces.Add(p);
        nonPremovingPieces.Remove(p);
    }


    public void healLivingPieces() {
        foreach (ChessPiece p in livingPieces) {
            p.health = Mathf.Min(p.maxHealth ,p.health+this.endurance);
            livingPieces.Add(p);
        }
    }
    public void revivePieces() {
        foreach (ChessPiece p in deadPieces) {
            p.health = Mathf.Max(1,this.endurance);
            livingPieces.Add(p);
        }
        this.deadPieces = new List<ChessPiece>();
    }

    public int getPieceDamage(int damage) {
        //2 things need to be calculated: crit chance and crit damage, lets say crit chance will just be luck/10
        //lets say crit damage will be just + .2 to the total damage, also always round up :)
        if(luck >= 10 || Random.Range(0, 10) < luck) {
            return Mathf.CeilToInt(damage * critDamage);
        }
        return damage;
        
    }
}
