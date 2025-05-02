using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PopupType {
    Damage, Block
}
public class PopUpManager : MonoBehaviour
{

    public Dictionary<PopupType, Image> popupImages;
    public void Start() {
        //init popupImages to map correct popup icon to visual element
    }


    public float duration = 1f;
    public void displayPopUp(PopupType imageToShow) {
        
    }
}
