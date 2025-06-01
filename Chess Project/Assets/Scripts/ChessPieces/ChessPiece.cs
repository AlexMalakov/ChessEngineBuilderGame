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
    getMoves, move, assignEffectiveDefense, getDefense, forceMove, takeDamage, onDeath, getPieceDamage, attack, onSacrifice, castle, promote
}

public abstract class ChessPiece : Entity
{
    public int effectiveDefense;

    //also includes defensive moves :)
    public Sprite activeSprite;
  
    //maps method name as string to pieceUpgrade
    protected Dictionary<PieceMethods, List<PieceUpgradeReward>> pieceUpgrades = new Dictionary<PieceMethods, List<PieceUpgradeReward>>(); 
    //some rewards care about if a piece is dead or not
    List<PieceUpgradeReward> deathListeners = new List<PieceUpgradeReward>();

    //gets all a piece's moves, and all pieces it can move to when "defending"
    public abstract List<Square> getDefensiveMoves();

    //gets all the moves a piece can take including sacrifices
    public abstract List<Square> getPossibleMoves(bool attacking);

    //executes the logic behind movement of a piece to the square square
    //returns true if succesful and false if not
    public virtual bool move(Square square) {
        if(this.pieceUpgrades.ContainsKey(PieceMethods.move)) {
            foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.move]) {
                if(upgrade.changeMove(this, square)) {
                    return true; //this is not correct but leaving it like this for now
                }
            }
        }
        
        foreach (Square s in getPossibleMoves(false)) {
            if (s.x == square.x && s.y == square.y) {
                StartCoroutine(this.slide(square));
                return true;
            }
        }

        throw new Exception("move not in possible moevs :'(");
        // return false;
    }

    //assigns effective defense to a piece; this lasts for a round of enemy attacks
    //in theory this method should be called before the next enemy attacks
    public void assignEffectiveDefense(List<ChessPiece> defenders) {
        int effectiveD = 0;
        foreach(ChessPiece d in defenders) {
            d.defend(this);
            effectiveD += d.getDefense();
        }


        NumberMultiplier multiplier = new NumberMultiplier(effectiveD);
        if(this.pieceUpgrades.ContainsKey(PieceMethods.assignEffectiveDefense)) {
            foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.assignEffectiveDefense]) {
                multiplier.addOperation(upgrade.changeEffectiveDefense(this, effectiveD));
            }
        }

        this.effectiveDefense = multiplier.resolve();

        StartCoroutine(this.popUpAction(PopupType.Block, this.effectiveDefense));

        foreach(ChessPiece d in defenders) {
            d.position.isAssisting(false);
        }
    }

    //gets a piece's defense
    public virtual int getDefense() {
        NumberMultiplier multiplier = new NumberMultiplier(this.defense);

        if(this.pieceUpgrades.ContainsKey(PieceMethods.getDefense)) {
            foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.getDefense]) {
                multiplier.addOperation(upgrade.changeDefense(this));
            }
        }

        return multiplier.resolve();
    }

    //moves a piece without checking if a move is possible
    //use for stuff like castleing, or for cc
    //WILL MOVE A PIECE, do not use unless nessisary
    public virtual void forceMove(Square square) {
        this.position.entity = null;
        this.position = square;
        this.position.entity = this;
        StartCoroutine(this.slide(square));
        //NOTE: THIS IS NOT BEING EFFECTED BY UPGRADES AT THE MOMENT
    }

    //causes a piece to take damage
    public override void takeDamage(int damage) {
        if(this.pieceUpgrades.ContainsKey(PieceMethods.takeDamage)) {
            foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.takeDamage]) {
                upgrade.changeTakeDamage(this, damage);
            }
        }

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
        foreach(PieceUpgradeReward r in this.deathListeners) {
            r.notifyOfDeath(this);
        }
        this.game.getBoard().onPieceTaken(this);
    }

    //gets the amount of damage a piece deals
    //at the moment just checks if it crits by asking the player
    public virtual int getPieceDamage(Square target) {
        NumberMultiplier damageCalculator = new NumberMultiplier(this.damage);

        if(this.pieceUpgrades.ContainsKey(PieceMethods.getPieceDamage)) {
            foreach(PieceUpgradeReward upgrade in this.pieceUpgrades[PieceMethods.getPieceDamage]) {
                damageCalculator.addOperation(upgrade.changePieceDamage(this, target));
            }
        }
        
        return damageCalculator.resolve();
    }

    //add more to this maybe idk
    public void onCrit() {
        StartCoroutine(this.popUpAction(PopupType.Crit, 0));
    }

    //performs an attack pop up, and sends the damage to the entity that is being attacked
    public virtual IEnumerator attack(Entity entity, List<ChessPiece> defenders) {
        if(entity.getEntityType() == EntityType.Piece) {
            Debug.Log("ERROR: ATTACKING ANOTHER CHESS PIECE?");
        }

        int damageToDeal = this.game.getPlayer().getPieceDamage(this, this.getPieceDamage(entity.position));

        foreach(ChessPiece p in defenders) {
            damageToDeal += p.getDefense();
            p.defend(this);
        }

        StartCoroutine(this.popUpAction(PopupType.Damage, damageToDeal));

        float elapsed = 0f;
        Vector3 initialScale = this.transform.localScale;
        Vector3 targetScale = initialScale * this.game.sizeIncrease;
        while(elapsed < this.game.pieceAttackGrowDuration) {
            this.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / this.game.pieceAttackGrowDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(.3f);

        yield return this.attackAnimation(entity.position);

        entity.takeDamage(damageToDeal);
        foreach(ChessPiece p in defenders) {
            p.position.isAssisting(false);
        }

        elapsed = 0f;
        while(elapsed < this.game.pieceAttackGrowDuration) {
            this.transform.localScale = Vector3.Lerp(targetScale, initialScale, elapsed / this.game.pieceAttackGrowDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    //small method that is called when a piece defends another piece
    public virtual IEnumerator defend(ChessPiece other) {
        this.position.isAssisting(true);
        return null;
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

    //on default the piece moves to it's target and back
    public virtual IEnumerator attackAnimation(Square opponentSq) {
        //check if there are any animation override requests
        //if yes choose the best one
        float elapsed = 0f;
        Vector3 start = transform.position;
        while(elapsed < this.game.playerAttackDuration) {
            transform.position = Vector3.Lerp(start, opponentSq.transform.position, elapsed/this.game.playerAttackDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;
        start = transform.position;
        while(elapsed < this.game.playerAttackDuration) {
            transform.position = Vector3.Lerp(start, this.position.transform.position, elapsed/this.game.playerAttackDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator popUpAction(PopupType type, int value) {
        yield return this.game.getPopUpManager().displayNumbers(type, value, this.transform);
    }

    public void addDeathListener(PieceUpgradeReward listener) {
        deathListeners.Add(listener);
    }

    public void removeDeathListener(PieceUpgradeReward listener) {
        deathListeners.Remove(listener);
    }
}
