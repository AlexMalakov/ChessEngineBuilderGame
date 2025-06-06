using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for now this will function similar to pop up manager, later it will manage animations probably
public class EffectManager : MonoBehaviour
{
    public GameObject effectObj;
    List<CustomEffect> customEffectPool = new List<CustomEffect>();
    int poolSize = 15;

    public void Start() {
        for(int i = 0; i < poolSize; i++) {
            GameObject newEffect = Instantiate(effectObj);
            this.customEffectPool.Add(newEffect.GetComponent<CustomEffect>());
        }
    }

    public CustomEffect getNextEffect() {
        foreach(CustomEffect c in this.customEffectPool) {
            if(!c.isActive()) {
                return c;
            }
        }

        GameObject newEffect = Instantiate(effectObj);
        CustomEffect effect = newEffect.GetComponent<CustomEffect>();
        this.customEffectPool.Add(effect);
        return effect;
    }

    public IEnumerator displayEffect(Entity target, string effect, float duration) {
        yield return getNextEffect().activate(target, effect, duration);
    }

}
