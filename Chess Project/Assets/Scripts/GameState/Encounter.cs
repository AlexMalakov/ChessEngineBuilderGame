using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    //holds enemy, board, 
    [Header ("game objects")]
    public GameObject boardObj;
    public GameObject enemyObj;
    public Transform boardStartPos;

    [Header ("encounter stuff")]

    public Enemy enemy; //replace this later
    public Board board;
    public Game game;
    public RewardSystem rewards;
    public SquareReporter sqReporter;
    public EncounterReporter encReporter;


    public virtual void startEncounter() {
        this.board = Instantiate(boardObj, boardStartPos).GetComponent<Board>();
        this.enemy = Instantiate(enemyObj).GetComponent<Enemy>();
        this.board.gameObject.SetActive(true);
        this.enemy.gameObject.SetActive(true);

        board.generateBoard();
        board.placePieces(this.game.getPieces());
        board.placeEntity(this.enemy);

        
        enemy.onEncounterStart(); //important that this happens before reporters
        sqReporter.onEncounterStart();
        encReporter.onEncounterStart();
        game.stratButton.onEncounterStart();
    }

    public virtual void onEnemyDefeat() {
        if(rewards != null) {
            this.rewards.displayRewards();
        } else {
            Debug.Log("ENCOUNTER HAS ENDED WITHOUT REWARDS!");
            game.onEncounterOver();
        }
    }

    public virtual void onRewardsOver() {
        game.onEncounterOver();
    }

    public virtual void startEnemyTurn() {
        enemy.takeTurn();
        //minoins attack?
    }

    public virtual IEnumerator startPlayerAttacks() {
        yield return board.assignEffectiveDefense();
        yield return board.performDamagePhase();
        this.enemy.returnDamage();
        this.game.startEnemyTurn();
    }



    // reporter info

    public virtual string getEncounterName() {
        return this.enemy.gameObject.name;
    }

    public virtual string getEncounterStats() {
        return "Health: " + this.enemy.health + "/" + this.enemy.maxHealth 
            + "\nDamage: " + this.enemy.damage + "\nDefense: " + this.enemy.defense;
    }
}
