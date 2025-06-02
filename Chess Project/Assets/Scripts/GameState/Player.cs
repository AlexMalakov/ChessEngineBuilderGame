using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Game game;
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
    public List<ChessPiece> temporaryPieces;
    public List<ChessPiece> tempPremovingPieces;
    public List<ChessPiece> tempNonPremovingPieces;

    [Header ("rewards")]
    public List<PieceUpgradeReward> myPieceUpgrades = new List<PieceUpgradeReward>();

    [Header ("other")]
    public PlayerReporter reporter;


    public void onTurnStart() {
        premovingPieces = new List<ChessPiece>();
        nonPremovingPieces = new List<ChessPiece>(livingPieces);
        this.reporter.onPlayerUpdate();
    }

    public void onPieceDeath(ChessPiece p) {
        livingPieces.Remove(p);
        premovingPieces.Remove(p);
        nonPremovingPieces.Remove(p);
        deadPieces.Add(p);
    }

    public void premovePiece(ChessPiece p) {
        if(livingPieces.Contains(p)) {
            premovingPieces.Add(p);
            nonPremovingPieces.Remove(p);
        } else if(temporaryPieces.Contains(p)) {
            tempPremovingPieces.Add(p);
            tempNonPremovingPieces.Remove(p);
        }
        
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

    public int getPieceDamage(ChessPiece p, int damage) {
        //2 things need to be calculated: crit chance and crit damage, lets say crit chance will just be luck/10
        //lets say crit damage will be just + .2 to the total damage, also always round up :)
        if(luck >= 10 || Random.Range(0, 10) < luck) {
            p.onCrit();
            return Mathf.CeilToInt(damage * (1+critDamage));
        }
        return damage;
        
    }

    public void upgradePieces(PieceUpgradeReward reward) {
        myPieceUpgrades.Add(reward);
        foreach(ChessPiece p in livingPieces) {
            if(reward.getPieceTarget() == PieceType.ChessPiece || reward.getPieceTarget() == p.getPieceType()) {
                p.mountPieceUpgrade(reward);
            }
        }
        foreach(ChessPiece p in deadPieces) {
            if(reward.getPieceTarget() == PieceType.ChessPiece || reward.getPieceTarget() == p.getPieceType()) {
                p.mountPieceUpgrade(reward);
            }
        }
    }

    public int getPlayerHP() {
        foreach(ChessPiece p in livingPieces) {
            if(p.getPieceType() == PieceType.King) {
                return p.health;
            }
        }
        return 0;
    }


    public void createTemporaryPiece(PieceType p, Square spawnSquare) {
        ChessPiece temporaryPiece = this.game.factory.createPiece(p, spawnSquare.x, spawnSquare.y);
        this.temporaryPieces.Add(temporaryPiece);
    }

    //returns true if hit
    public bool rollWithLuck(float targetThreshold, bool positiveLuck) {
        //should be rewritten in the future to actually use luck lmao
        float rng = Random.Range(0f, 1f);

        return rng < targetThreshold;
    }

    //strategy stuff
    public bool kingStrategyHasActive() {
        foreach(ChessPiece p in this.livingPieces) {
            if(p.getPieceType() == PieceType.King) {
                return ((King)p).canActivateStrategy();
            }
        }
        Debug.Log("COULD NOT FIND KING!");
        return false;
    }

    public void activateKingStrategyAbility() {
        foreach(ChessPiece p in this.livingPieces) {
            if(p.getPieceType() == PieceType.King) {
                ((King)p).activateCurrentStrategy();
                return;
            }
        }
        Debug.Log("COULD NOT FIND KING!");
    }

    public bool canSwapKingStrategy() {
        foreach(ChessPiece p in this.livingPieces) {
            if(p.getPieceType() == PieceType.King) {
                return ((King)p).canSwapStrategy();
            }
        }
        Debug.Log("COULD NOT FIND KING!");
        return false;
    }

    public void swapKingStrategy() {
        foreach(ChessPiece p in this.livingPieces) {
            if(p.getPieceType() == PieceType.King) {
                ((King)p).swapStrategy();
                return;
            }
        }
        Debug.Log("COULD NOT FIND KING!");
    }

    public List<KingStance> getCurrentStrategies() {
        foreach(ChessPiece p in this.livingPieces) {
            if(p.getPieceType() == PieceType.King) {
                return ((King)p).getStrategies();
            }
        }
        Debug.Log("COULD NOT FIND KING!");
        return new List<KingStance>();
    }

    public virtual void addKingStrategy(KingStance swappingIn, KingStance swappingOut) {
        foreach(ChessPiece p in this.livingPieces) {
            if(p.getPieceType() == PieceType.King) {
                ((King)p).addStrategy(swappingIn, swappingOut);
                return;
            }
        }
        Debug.Log("COULD NOT FIND KING!");
    }
}
