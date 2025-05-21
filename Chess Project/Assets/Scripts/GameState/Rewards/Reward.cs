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
    public RewardSystem rewardSystem;
    public Game game;
    public Outline outline;
    public Button button;


    //to use reflection, 
    public void init(RewardSystem reward, Game game) {
        this.rewardSystem = reward;
        this.game = game;
    }

    public void Start() {
        Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(onButtonClick);
    }

    public abstract void applyEffect();

    public virtual void onButtonClick()
    {
        this.rewardSystem.onRewardSelected(this);
    }

    public void toggleOutline(bool toggle) {
        this.outline.enabled = toggle;
    }

    public void assignUI(Outline outline, Button button) {
        this.outline = outline;
        this.button = button;
    }

    public void unassignUI() {
        this.outline = null;
        this.button = null;
    }

    public bool isActive() {
        return this.outline != null;
    }
}
