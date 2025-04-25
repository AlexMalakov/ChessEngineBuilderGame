using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Game game;
    public int startingX; public int startingY;
    public int health;
    public int maxHealth;
    public int defense;
    public int damage;
    public Square position;


    public abstract void onDeath();

    public virtual void takeDamage(int damage) {
        health-=damage;

        if(health <= 0) {
            this.onDeath();
        }
    }

    public virtual IEnumerator slide(Square toSquare) {
        this.position = toSquare;
        yield return this.game.getBoard().slideObj(this.gameObject, toSquare);
    }

    public virtual void place(Transform t) {
        this.transform.position = t.position;
    }
}
