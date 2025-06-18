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
    public SquareReporter sqReporter;
    public EncounterReporter encReporter;
    [Header ("reward stuff")]
    public RewardType rewardType;
    public Rarity rewardRarity;
    public int rewardCount;


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


        foreach(PieceUpgradeReward r in game.getPlayer().myPieceUpgrades) {
            r.onEncounterStart();
        }
    }

    public virtual void onEnemyDefeat() {
        this.game.rewardSystem.displayRewards(rewardType, rewardRarity, rewardCount);
    }

    public virtual void onRewardsOver() {
        game.onEncounterOver();
    }

    public virtual void startEnemyTurn() {
        StartCoroutine(enemy.takeTurn());
        //minoins attack?
    }

    public virtual IEnumerator startPlayerAttacks() {
        yield return board.assignEffectiveDefense();
        yield return board.performDamagePhase();
        this.enemy.returnDamage();

        if(this.enemy.health > 0) {
            this.game.startEnemyTurn();
        }
    }



    // reporter info

    public virtual string getEncounterName() {
        return this.enemy.gameObject.name;
    }

    public virtual List<string[]> getEncounterStats() {
        List<string[]> stats = new List<string[]>();
        stats.Add(new string[]{this.enemy.name, this.enemy.health + "/" + this.enemy.maxHealth, this.enemy.damage + "", this.enemy.defense + ""});
        return stats;
        // return "Health: " + this.enemy.health + "/" + this.enemy.maxHealth 
        //     + "\nDamage: " + this.enemy.damage + "\nDefense: " + this.enemy.defense;
    }
}
