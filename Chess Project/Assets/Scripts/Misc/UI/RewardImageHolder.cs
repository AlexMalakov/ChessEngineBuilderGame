using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardImageHolder : MonoBehaviour
{
    public Image image;
    public PieceUpgradeReward reward;

    public void assignReward(PieceUpgradeReward r) {
        this.reward = r;
        this.image = r.getRewardImage();
    }
}
