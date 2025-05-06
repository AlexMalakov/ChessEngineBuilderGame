using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatReward : Reward
{
    public string stat;
    public int increaseAmount = 1;

    public override void applyEffect() {
        if(stat == "agility") {
            this.game.getPlayer().agility += increaseAmount;
        } else if(stat == "luck") {
            this.game.getPlayer().luck += increaseAmount;
        } else if(stat == "endurance") {
            this.game.getPlayer().endurance += increaseAmount;
        } else if(stat == "perception") {
            this.game.getPlayer().perception += increaseAmount;
        } else if(stat == "????") {
            this.game.getPlayer().luck += increaseAmount;//placeholder
        }

        this.game.getPlayer().reporter.onPlayerUpdate();
        
    }
}
