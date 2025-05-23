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

    public static List<Reward> getStatRewards(int rarityNum) {
        List<Reward> rewards = new List<Reward>();
        rewards.Add(new StatReward("agility", rarityNum));
        rewards.Add(new StatReward("luck", rarityNum));
        rewards.Add(new StatReward("endurance", rarityNum));
        rewards.Add(new StatReward("perception", rarityNum));
        return rewards;
    }

    public override string getRewardName() {
        return this.stat;
    }
    public override string getRewardDescription() {
        if(stat == "agility") {
            return "The number of piece actions before the end of your turn";
        } else if(stat == "luck") {
            return "Affects all RNG rolls performed in the game, boosting your rolls and dropping enemy rolls";
        } else if(stat == "endurance") {
            return "Grants pieces bonus max-hp and healing";
        } else if(stat == "perception") {
            return "Grants insight on enemy actions and grants premoves for every two perception";
        }
        return "????";
    }
    public override string getRewardFlavorText() {
        if(stat == "agility") {
            return "work faster, not smarter";
        } else if(stat == "luck") {
            return "99% of gamblers quit right before they win big";
        } else if(stat == "endurance") {
            return "bigger number better person";
        } else if(stat == "perception") {
            return "read like a book";
        }

        return "????? :(";
    }
    public override string getRewardImage() {
        return this.stat;
    }
}
