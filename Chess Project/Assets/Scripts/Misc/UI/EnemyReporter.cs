using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyReporter : MonoBehaviour
{
    public Game game;
    public TMP_Text enemyName;
    public TMP_Text enemyStats;
    public Image enemyImage;



    public void onEncounterStart() {
        enemyName.text = game.getEnemy().gameObject.name;
        enemyStats.text = "Health: " + game.getEnemy().health + "/" + game.getEnemy().maxHealth + "\nDamage: " + game.getEnemy().damage + "\nDefense: " + game.getEnemy().defense;

        enemyImage.sprite = game.getEnemy().enemySprite;
        enemyImage.type = Image.Type.Simple;
        enemyImage.preserveAspect = false;
    }

    public void onStatUpdate() {
        enemyStats.text = "Health: " + game.getEnemy().health + "/" + game.getEnemy().maxHealth + "\nDamage: " + game.getEnemy().damage + "\nDefense: " + game.getEnemy().defense;
    }
}
