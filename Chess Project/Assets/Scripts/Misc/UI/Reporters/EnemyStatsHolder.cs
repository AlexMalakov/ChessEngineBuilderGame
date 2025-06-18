using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyStatsHolder : MonoBehaviour
{
   public TMP_Text health;
   public TMP_Text damage;
   public TMP_Text nameText;
   public TMP_Text defense;

   public void init(string name, string health, string damage, string defense) {
        this.nameText.text = name;
        this.health.text = health;
        this.damage.text = damage;
        this.defense.text = defense;
   }
}
