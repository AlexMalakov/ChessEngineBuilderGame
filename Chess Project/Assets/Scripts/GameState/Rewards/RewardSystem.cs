using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;

public class RewardSystem : MonoBehaviour
{
    public List<Reward> possibleRewards;
    public Encounter encounter;
    public GameObject rewardCanvas;
    public transform rewardParent;
    public GameObject rewardHolder;


    private Reward selectedReward;

    public void Awake() {
        this.possibleRewards = pieceUpgrades();
    }

    public void displayRewards(RewardType type, Rarity rarity, int amount) {
        List<Reward> displayingR = selectedPosibleRewards(type, rarity, amount);
        selectedReward = null;
        this.rewardCanvas.SetActive(true);
    }

    public void onRewardSelected(Reward reward) {
        if(this.selectedReward) {
            this.selectedReward.toggleOutline(false);
        }
        
        this.selectedReward = reward;
        this.selectedReward.toggleOutline(true);
    }

    public void onRewardLockin() {
        if(this.selectedReward == null) {
            return; //this will do nothing unless a reward is chosen
        }

        foreach(Reward r in this.possibleRewards) {
            r.unassignUI();
        }
        this.rewardCanvas.SetActive(false);
        this.selectedReward.applyEffect();
        this.encounter.onRewardsOver();
    }

    private List<PieceUpgradeReward> pieceUpgrades() {
        List<Type> subclasses = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(PieceUpgradeReward)) && !type.IsAbstract)
            .ToList();
        
        List<PieceUpgradeReward> allPieceUpgrades = new List<PieceUpgradeReward>();

        foreach(var subclass in subclasses) {
            PieceUpgradeReward r = Activator.CreateInstance(subclass) as PieceUpgradeReward;
            if(r != null) {
                r.init(this, this.game);
                allPieceUpgrades.Add(r);
            }
        }

        return allPieceUpgrades;
    }

    private List<Reward> selectedPosibleRewards(RewardType type, Rarity rarity, int amount) {
        switch(type) {
            case RewardType.Stat:
                return StatReward.getStatRewards(rarity);
            case RewardType.Upgrade:
                List<Reward> chosenRewards = new List<Reward>();;
                while(true) {
                    if(chosenRewards.Count >= amount) {
                        break;
                    }

                    Reward rngReward = this.possibleRewards[Random.Range(0, this.possibleRewards.Count)];
                    if(!rngReward.isActive()) {
                        //assign UI
                        GameObject uiElement = Instantiate(rewardHolder, rewardParent);
                        rngReward.assignUI(rewardHolder.GetComponent<Outline>(), rewardHolder.GetComponent<Button>());
                        chosenRewards.Add(rngReward);
                    }
                }
                break;
        }
    }
}
