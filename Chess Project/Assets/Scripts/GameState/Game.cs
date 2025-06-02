using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header ("important objects")]
    public Player player;
    public PopUpManager popUpManager;
    public RewardSystem rewardSystem;
    public List<Encounter> encouters;
    public PieceFactory factory;
    public FakeMono mono;
    public Graveyard graveyard;

    [Header ("important data")]
    public bool playersTurn;
    public bool playerPremoveTurn;
    public int playerPremoves;
    public int playerMoves;

    [Header ("config stuff")]
    public float playerAttackDuration;
    public float pieceAttackGrowDuration;
    public float sizeIncrease;
    public float defendPopUpDuration;

    [Header ("round over listeners")]
    public TurnButton turnButton;
    public StrategyCycleButton stratButton;
    public List<PieceUpgradeReward> roundListeners = new List<PieceUpgradeReward>();

    int currentEncounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        startEncounter();
        startPlayerTurn();
    }

    public void addRoundOverListener(PieceUpgradeReward roundListener) {
        roundListeners.Add(roundListener);
    }
    public void removeRoundOverListener(PieceUpgradeReward roundListener) {
        roundListeners.Remove(roundListener);
    }

    public void startPlayerTurn() {
        foreach(PieceUpgradeReward r in this.roundListeners) {
            r.notifyRoundOver();
        }
        playersTurn = true;
        playerMoves = player.agility;
        player.onTurnStart(); //removes premove status from pieces
        turnButton.onTurnUpdate();
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
        player.reporter.onPlayerUpdate();
    }

    public void endPlayerTurn() {
        playersTurn = false;
        if(player.perception / 2 > 0) {
            this.startPlayerPremoves();
        } else {
            this.startPlayerAttacks();
        }
        
    }

    public void forceTurnOver() {
        if(playersTurn) {
            this.endPlayerTurn();
        } else {
            this.endPlayerPremoveTurn();
        }
    }

    public void startPlayerAttacks() {
        turnButton.onTurnUpdate();
        StartCoroutine(this.encouters[currentEncounter].startPlayerAttacks());
    }

    public void startPlayerPremoves() {
        this.playerPremoveTurn = true;
        this.playerPremoves = player.perception / 2;
        turnButton.onTurnUpdate();
    }

    public void onPlayerPremove() {
        playerPremoves--;
        if(playerPremoves <= 0) {
            this.endPlayerPremoveTurn();
        }
    }

    public void endPlayerPremoveTurn() {
        playerPremoveTurn = false;
        this.startPlayerAttacks();
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
        List<ChessPiece> pieces = new List<ChessPiece>();
        pieces.AddRange(this.player.livingPieces);
        pieces.AddRange(this.player.temporaryPieces);
        return pieces;
    }

    public List<ChessPiece> getNonPremovePieces() {
        List<ChessPiece> move = new List<ChessPiece>();
        move.AddRange(this.player.nonPremovingPieces);
        move.AddRange(this.player.tempNonPremovingPieces);
        return move;
    }

    public List<ChessPiece> getPremovePieces() {
        List<ChessPiece> preMove = new List<ChessPiece>();
        preMove.AddRange(this.player.premovingPieces);
        preMove.AddRange(this.player.tempPremovingPieces);
        return preMove;
    }

    public Player getPlayer() {
        return this.player;
    }

    public PopUpManager getPopUpManager() {
        return this.popUpManager;
    }

}
