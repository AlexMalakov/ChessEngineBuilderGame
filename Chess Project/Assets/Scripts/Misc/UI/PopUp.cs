using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    public GameObject damage;
    public GameObject block;
    public GameObject crit;
    public TMP_Text text;

    bool active;

    public void activate(PopupType type, int value) {
        text.text = ""+value;
        text.color = Color.black;// or new Color(r, g, b, a)
        switch(type) {
            case PopupType.Damage:
                damage.SetActive(true);
                // text.fontMaterial.SetColor("_OutlineColor", Color.red);
                break;
            case PopupType.Block:
                block.SetActive(true);
                // text.fontMaterial.SetColor("_OutlineColor", Color.blue);
                break;
        }
        // text.fontMaterial.SetFloat("_OutlineWidth", 0.2f);

        this.setActive(true);
    }

    public void setActive(bool active) {
        this.active = active;
        this.gameObject.SetActive(active);
    }

    public bool isActive() {
        return this.active;
    }
}
