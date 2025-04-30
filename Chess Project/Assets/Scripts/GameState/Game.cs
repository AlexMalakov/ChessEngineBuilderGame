using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Player player;
    public List<Encounter> encouters;

    int currentEncounter = 0;
    public Graveyard graveyard;
    public bool playersTurn;
    public bool playerPremoveTurn;
    public int playerPremoves;
    public int playerMoves;
    // Start is called before the first frame update
    void Start()
    {
        startEncounter();
        startPlayerTurn();
    }


    public void startPlayerTurn() {
        playersTurn = true;
        playerMoves = player.agility;
        player.onTurnStart(); //removes premove status from pieces
    }

    public void startEnemyTurn() {
        this.encouters[currentEncounter].startEnemyTurn();
        //notify the enemy;
    }

    public void onPlayerMove() {
        playerMoves--;
        if(playerMoves <= 0) {
            this.endPlayerTurn();
        }
    }

    public void endPlayerTurn() {
        playersTurn = false;
        if(player.perception / 2 > 0) {
            this.startPlayerPremoves();
        } else {
            this.startPlayerAttacks();
        }
        
    }

    public void startPlayerAttacks() {
        StartCoroutine(this.encouters[currentEncounter].startPlayerAttacks());
    }

    public void endPlayerAttacks() {
        this.startEnemyTurn();
    }

    public void startPlayerPremoves() {
        this.playerPremoveTurn = true;
        this.playerPremoves = player.perception / 2;
    }

    public void onPlayerPremove() {
        playerPremoves--;
        if(playerPremoves <= 0) {
            playerPremoveTurn = false;
            this.startPlayerAttacks();
        }
    }

    public void onEncounterOver() {
        currentEncounter++;
        //between encounter logic
        startEncounter();
    }

    public void onPlayerDefeat() {
        //defeat message idk
    }

    public void startEncounter() {
        this.encouters[currentEncounter].startEncounter();
    }

    public Encounter getEncounter() {
        return this.encouters[currentEncounter];
    }

    public Enemy getEnemy() {
        return this.encouters[currentEncounter].enemy;
    }

    public Board getBoard() {
        return this.encouters[currentEncounter].board;
    }

    public List<ChessPiece> getPieces() {
        return this.player.livingPieces;
    }

    public List<ChessPiece> getNonPremovePieces() {
        return this.player.nonPremovingPieces;
    }

    public List<ChessPiece> getPremovePieces() {
        return this.player.premovingPieces;
    }

    public Player getPlayer() {
        return this.player;
    }

}
