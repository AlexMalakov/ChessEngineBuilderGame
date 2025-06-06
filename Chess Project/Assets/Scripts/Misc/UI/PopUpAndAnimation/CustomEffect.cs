using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEffect : MonoBehaviour
{
    public List<string> possibleEffects;
    public List<GameObject> childEffects;
    bool active = false;

    public IEnumerator activate(Entity target, string effect, float duration) {
        active = true;
        this.transform.position = target.transform.position;
        childEffects[possibleEffects.IndexOf(effect)].SetActive(true);
        yield return new WaitForSeconds(duration);
        childEffects[possibleEffects.IndexOf(effect)].SetActive(false);
        active = false;
    }

    public bool isActive() {
        return active;
    }
}
