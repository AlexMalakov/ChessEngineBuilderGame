using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RewardType {
    Stat,
    Upgrade,
    Item,
}
public enum Rarity {
    Common = 1,
    Uncommon = 2, 
    Rare = 3,
    Epic = 5,
    Legandary = 7,
}
public abstract class Reward
{
    public Rarity lootRarity; //needs to be initialized
    // public RewardSystem rewardSystem;
    public Game game;
    public RewardSelector selector;
    public bool selected = false;

    //to use reflection, 
    public void init(Game game, RewardSelector selector) {
        this.game = game;
        this.selector = selector;
    }

    public abstract void applyEffect();

    public abstract string getRewardName();
    public abstract string getRewardDescription();
    public abstract string getRewardFlavorText();
    public abstract string getRewardImage();
}
