using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : Entity
{



    public void onSummon() {
        health = maxHealth;
    }

    public void takeTurn() {

    }


    public override void onDeath() {

    }
}
