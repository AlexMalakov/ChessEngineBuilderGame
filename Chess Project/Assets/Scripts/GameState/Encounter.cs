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


    public void startEncounter() {
        this.board = Instantiate(boardObj, boardStartPos).GetComponent<Board>();
        this.enemy = Instantiate(enemyObj).GetComponent<Enemy>();
        this.board.gameObject.SetActive(true);
        this.enemy.gameObject.SetActive(true);

        board.generateBoard();
        board.placePieces(this.game.getPieces());
        board.placeEntity(this.enemy);
        enemy.onEncounterStart();
    }

    public void onEnemyDefeat() {
        if(rewards != null) {
            this.rewards.displayRewards();
        } else {
            Debug.Log("ENCOUNTER HAS ENDED WITHOUT REWARDS!");
            game.onEncounterOver();
        }
    }

    public void onRewardsOver() {
        game.onEncounterOver();
    }

    public void startEnemyTurn() {
        enemy.takeTurn();
        //minoins attack?
    }

    public IEnumerator startPlayerAttacks() {
        yield return board.assignEffectiveDefense();
        yield return enemy.takeDamage(board.calculateDamage(enemy.position));
    }
}
