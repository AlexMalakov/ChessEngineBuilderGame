using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerReporter : MonoBehaviour
{
    public Game game;

    public TMP_Text actionsAndHealthInfo;

    [Header ("stats")]
    public TMP_Text agilityInfo;
    public TMP_Text percepInfo;
    public TMP_Text luckInfo;
    public TMP_Text endurInfo;

    public void onPlayerUpdate() {
        if(this.game.playerPremoveTurn) {
            this.actionsAndHealthInfo.text = "HP " + game.getPlayer().getPlayerHP() + "  Pre-Moves " + game.playerPremoves;
        } else {
            this.actionsAndHealthInfo.text = "HP " + game.getPlayer().getPlayerHP() + "  Moves " + game.playerMoves;
        }
        agilityInfo.text = "" + game.getPlayer().agility;
        percepInfo.text = "" + game.getPlayer().perception;
        luckInfo.text = "" + game.getPlayer().luck;
        endurInfo.text = "" + game.getPlayer().endurance;
    }
}
