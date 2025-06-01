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
    List<PopUp> popUpPool = new List<PopUp>();
    public int poolSize = 45;
    public float duration = 1f;
    
    public void Start() {
        //init popupImages to map correct popup icon to visual element
        for(int i = 0; i < poolSize; i++) {
            GameObject newPopUp = Instantiate(popUp);
            this.popUpPool.Add(newPopUp.GetComponent<PopUp>());
            newPopUp.GetComponent<PopUp>().setActive(false);
        }
    }

    public PopUp getNextPopUp() {
        foreach(PopUp p in this.popUpPool) {
            if(!p.isActive()) {
                return p;
            }
        }

        GameObject newPopUp = Instantiate(popUp);
        PopUp popup = newPopUp.GetComponent<PopUp>();
        this.popUpPool.Add(popup);
        popup.setActive(false);
        return popup;
    }

    public IEnumerator displayNumbers(PopupType imageToShow, int value, Transform popUpLocation) {
        PopUp newPopUp = getNextPopUp();
        newPopUp.transform.SetParent(popUpLocation);
        newPopUp.transform.localPosition = Vector3.zero;
        newPopUp.activateNumbers(imageToShow, value);

        yield return new WaitForSeconds(duration);
        newPopUp.setActive(false);
    }

    public PopUp displayStatusEffect(StatusEffect status, Transform popUpLocation) {
        PopUp statusHolder = getNextPopUp();
        statusHolder.transform.SetParent(popUpLocation);
        statusHolder.transform.localPosition = Vector3.zero;
        statusHolder.activateStatus(status);

        return statusHolder;
    }
}
