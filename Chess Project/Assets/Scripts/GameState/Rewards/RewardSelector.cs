using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardSelector : MonoBehaviour
{
    public RewardSystem rSystem;
    private Reward reward;

    [Header ("sprites")]
    public List<string> dictionaryImagesKeys;
    public List<Sprite> dictionaryImagesValues;

    [Header ("UI PARTS")]
    public Button button;
    public Outline outline;
    public Image image;
    public TMP_Text rewardName;
    public TMP_Text rewardDescription;
    public TMP_Text rewardFlavorText;

    public void Start() {
        Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(onButtonClick);
    }

    public virtual void onButtonClick()
    {
        this.rSystem.onRewardSelected(reward);
    }

    public void toggleOutline(bool toggle) {
        this.outline.enabled = toggle;
    }

    public void assignReward(Reward r) {
        this.reward = r;
        this.rewardName.text = r.getRewardName();
        this.rewardDescription.text = r.getRewardDescription();
        this.rewardFlavorText.text = r.getRewardFlavorText();

        this.image.sprite = this.dictionaryImagesValues[this.dictionaryImagesKeys.IndexOf(r.getRewardImage())];
        this.image.type = Image.Type.Simple;
        this.image.preserveAspect = false;
    }
}