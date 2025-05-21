using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatReward : Reward
{
    public string stat;
    public int increaseAmount;

    public StatReward(string stat, int increaseAmount) {
        this.stat = stat;
        this.increaseAmount = increaseAmount;
    }

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

    public static List<StatReward> getStatRewards(int rarityNum) {
        List<StatReward> rewards = new List<StatReward>();
        rewards.Add(new StatReward("agility", rarityNum));
        rewards.Add(new StatReward("luck", rarityNum));
        rewards.Add(new StatReward("endurance", rarityNum));
        rewards.Add(new StatReward("perception", rarityNum));
        return rewards;
    }
}
