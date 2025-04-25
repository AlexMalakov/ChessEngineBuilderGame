using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Game game;
    public int startingX; public int startingY;
    public int health;
    public int maxHealth;
    public int defense;
    public int damage;
    public Square position;

    public Projectile projectile;

    public List<EnemyAction> actionQueue;
    public List<EnemyAction> actionLoop;
    int currentAct = 0;

    public List<ChessPiece> attackers;
    public Sprite enemySprite;
    public EnemyReporter reporter;

    
    public void onEncounterStart() {
        this.reporter.onEncounterStart();
    }

    public void takeTurn() {
        if(currentAct < actionQueue.Count) {
            StartCoroutine(actionQueue[currentAct].takeAction());
        } else {
            StartCoroutine(actionLoop[(currentAct - actionQueue.Count)%actionLoop.Count].takeAction());
        }
        currentAct++;
    }

    public void onTurnOver() {
        this.game.startPlayerTurn();
    }

    public void takeDamage(int damage/*, ChessPiece attacker*/) {
        // if(attacker != null) {
        //     this.attackers.Add(attacker);
        // }

        health-=damage;
        game.getBoard().returnDamage(this.position,this.defense);

        if(health <= 0) {
            this.game.getEncounter().onEnemyDefeat();
        }

        this.reporter.onStatUpdate();
    }

    public IEnumerator slide(Square toSquare) {
        this.position.enemy = null;
        this.position = toSquare;
        // toSquare.enemy = this;
        yield return this.game.getBoard().slideObj(this.gameObject, toSquare);
    }

    public void place(Transform t) {
        this.transform.position = t.position;
    }
}
