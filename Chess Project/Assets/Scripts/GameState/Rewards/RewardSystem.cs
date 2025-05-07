using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    public List<Reward> possibleRewards;
    public Encounter encounter;
    public GameObject rewardCanvas;


    private Reward selectedReward;

    public void displayRewards() {
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
        this.rewardCanvas.SetActive(false);
        this.selectedReward.applyEffect();
        this.encounter.onRewardsOver();
    }
}
