using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PopupType {
    Damage, Block, DamageTaken
}
public class PopUpManager : MonoBehaviour
{

    public GameObject popUp;
    public float duration = 1f;
    
    public void Start() {
        //init popupImages to map correct popup icon to visual element
    }

    public IEnumerator displayPopUp(PopupType imageToShow, int value, Transform popUpLocation) {
        GameObject newPopUp = Instantiate(popUp, popUpLocation);
        newPopUp.GetComponent<PopUp>().activate(imageToShow, value);
        yield return new WaitForSeconds(duration);
        Destroy(newPopUp);
    }
}
