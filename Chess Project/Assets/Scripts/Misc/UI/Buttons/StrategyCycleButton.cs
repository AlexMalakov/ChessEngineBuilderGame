using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrategyCycleButton : MonoBehaviour
{
    public Game game;
    public Button button;
    public GameObject buttonObj;

    public void Start() {
        Button btn = button.GetComponent<Button>();
		btn.onClick.AddListener(onButtonClick);
    }

    public void onEncounterStart() {
        this.buttonObj.SetActive(this.game.getPlayer().canSwapKingStrategy());
    }

    public void onButtonClick()
    {
        if(this.game.getPlayer().canSwapKingStrategy()) {
            this.game.getPlayer().swapKingStrategy();
        }
    }

}
