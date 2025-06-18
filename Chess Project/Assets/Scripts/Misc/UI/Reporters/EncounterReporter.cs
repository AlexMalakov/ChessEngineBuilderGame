using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EncounterReporter : MonoBehaviour
{
    public Game game;
    public TMP_Text enemyName;
    public Image enemyImage;
    public EnemyStatsHolder statsHolder;

    public void onEncounterStart() {
        enemyName.text = game.getEncounter().getEncounterName();
        List<string[]> stats = game.getEncounter().getEncounterStats();
        foreach(string[] stat in stats) {
            EnemyStatsHolder holder = Instantiate(statsHolder, statsHolder.transform.parent);
            holder.gameObject.SetActive(true);
            holder.init(stat[0], stat[1], stat[2], stat[3]);
        }
        // enemyStats.text = game.getEncounter().getEncounterStats();

        enemyImage.sprite = game.getEnemy().enemySprite;
        enemyImage.type = Image.Type.Simple;
        enemyImage.preserveAspect = false;
    }

    public void onStatUpdate() {
        // enemyStats.text = game.getEncounter().getEncounterStats();

        List<string[]> stats = game.getEncounter().getEncounterStats();
        foreach(string[] stat in stats) {
            EnemyStatsHolder holder = Instantiate(statsHolder, statsHolder.transform.parent);
            holder.gameObject.SetActive(true);
            holder.init(stat[0], stat[1], stat[2], stat[3]);
        }
    }
}
