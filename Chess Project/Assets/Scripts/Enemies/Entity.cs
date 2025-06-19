using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EntityType {
    Piece, Enemy, Minion,
}
public abstract class Entity : MonoBehaviour
{
    [Header ("game")]
    public Game game;
    public int startingX; public int startingY;
    public Square position;
    [Header ("stats")]
    public int health;
    public int maxHealth;
    public int defense;
    public int damage;
    [Header ("status effects")]
    public List<StatusEffect> statuses = new List<StatusEffect>();


    public abstract void onDeath();

    public virtual void takeDamage(int damage) {
        health-=damage;

        StartCoroutine(this.game.getPopUpManager().displayNumbers(PopupType.DamageTaken, damage, this.transform));

        if(health <= 0) {
            this.onDeath();
        }
    }

    public virtual void addStatusEffect(StatusEffect effect) {
        this.statuses.Add(effect);
    }

    public virtual void removeStatusEffect(StatusEffect effect) {
        this.statuses.Remove(effect);
    }

    public virtual IEnumerator slide(Square toSquare) {
        this.position.entity = null;
        toSquare.entity = this;
        this.position = toSquare;
        yield return this.game.getBoard().slideObj(this.gameObject, toSquare);
    }

    public virtual void place(Transform t) {
        this.transform.position = t.position;
    }

    public abstract EntityType getEntityType();
}
