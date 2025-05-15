using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EncounterReporter : MonoBehaviour
{
    public Game game;
    public TMP_Text enemyName;
    public TMP_Text enemyStats;
    public Image enemyImage;



    public void onEncounterStart() {
        enemyName.text = game.getEncounter().getEncounterName();
        enemyStats.text = game.getEncounter().getEncounterStats();

        enemyImage.sprite = game.getEnemy().enemySprite;
        enemyImage.type = Image.Type.Simple;
        enemyImage.preserveAspect = false;
    }

    public void onStatUpdate() {
        enemyStats.text = game.getEncounter().getEncounterStats();
    }
}
