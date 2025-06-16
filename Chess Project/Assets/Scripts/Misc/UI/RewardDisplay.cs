using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardDisplay : MonoBehaviour
{
    public GameObject rewardImageHolder;
    public Transform startingPoint;
    public float yOffset;
    private int count = 0;




    public void onNewReward(PieceUpgradeReward r) {
        GameObject newHolder = Instantiate(rewardImageHolder, this.transform);

        newHolder.GetComponent<RewardImageHolder>().assignReward(r);
        newHolder.SetActive(true);
        newHolder.Position = startingPoint.position + new Vector3(0, -offset * yOffset, 0);

        count++;

    }
}
