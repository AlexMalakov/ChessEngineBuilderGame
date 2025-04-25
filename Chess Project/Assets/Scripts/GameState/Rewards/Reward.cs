using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Rarity {
    Common,
    Uncommon, 
    Rare,
    Epic,
    Legandary,
}
public abstract class Reward : MonoBehaviour
{
    public Rarity lootRarity;
    public RewardSystem rewardSystem;
    public Game game;
    public Outline outline;
    public Button button;


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
}
