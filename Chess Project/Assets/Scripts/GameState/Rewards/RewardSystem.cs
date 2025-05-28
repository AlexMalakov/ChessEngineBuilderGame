using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using UnityEngine.UI;

public class RewardSystem : MonoBehaviour
{
    public List<Reward> possibleRewards;
    public Encounter encounter;
    public GameObject rewardCanvas;
    public Transform rewardParent;
    public GameObject rewardHolder;
    public GameObject rewardHolderHolder;


    private Reward selectedReward;

    public void Awake() {
        this.possibleRewards = pieceUpgrades();
    }

    public void displayRewards(RewardType type, Rarity rarity, int amount) {
        List<Reward> displayingR = selectedPosibleRewards(type, rarity, amount);

        foreach(Reward r in displayingR) {
            RewardSelector selector = Instantiate(rewardHolder, rewardHolderHolder.transform).GetComponent<RewardSelector>();
            selector.gameObject.SetActive(true);
            r.init(this.encounter.game, selector);
            selector.assignReward(r);
        }

        selectedReward = null;
        this.rewardCanvas.SetActive(true);
    }

    public void onRewardSelected(Reward reward) {
        if(this.selectedReward != null) {
            this.selectedReward.selector.toggleOutline(false);
        }

        this.selectedReward = reward;
        this.selectedReward.selector.toggleOutline(true);
    }

    public void onRewardLockin() {
        if(this.selectedReward == null) {
            return; //this will do nothing unless a reward is chosen
        }

        foreach(Reward r in this.possibleRewards) {
            r.selected = false;
            if(r.selector != null) {
                Destroy(r.selector);
                r.selector = null;
            }
        }

        this.possibleRewards.Remove(selectedReward);

        this.rewardCanvas.SetActive(false);
        this.selectedReward.applyEffect();
        this.encounter.onRewardsOver();
    }

    private List<Reward> pieceUpgrades() {
        List<Type> subclasses = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(PieceUpgradeReward)) && !type.IsAbstract)
            .ToList();
        
        List<Reward> allPieceUpgrades = new List<Reward>();

        foreach(var subclass in subclasses) {
            PieceUpgradeReward r = Activator.CreateInstance(subclass) as PieceUpgradeReward;
            if(r != null) {
                allPieceUpgrades.Add(r);
            }
        }

        return allPieceUpgrades;
    }

    private List<Reward> selectedPosibleRewards(RewardType type, Rarity rarity, int amount) {
        switch(type) {
            case RewardType.Stat:
                return StatReward.getStatRewards((int)rarity);
            case RewardType.Upgrade:
                List<Reward> chosenRewards = new List<Reward>();;
                while(true) {
                    if(chosenRewards.Count >= amount) {
                        break;
                    }

                    Reward rngReward = this.possibleRewards[UnityEngine.Random.Range(0, this.possibleRewards.Count)];
                    if(!rngReward.selected) {
                        //assign UI
                        rngReward.selected = true;
                        chosenRewards.Add(rngReward);
                    }
                }
                return chosenRewards;
        }
        Debug.Log("NO REWARDS WERE SELECTED!");
        return new List<Reward>();
    }
}
