using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic_EliminateUnits : RoomLogic {

    public List<Health> Units;
    public GameObject PostFightElements;

    bool active = true;

    public void Update() {
        if(!active) return;

        foreach(Health baby in Units) {
            if(baby != null) return;
        }

        OpenExitWithDelay(2);
        StartCoroutine(EnabledPostFightElementsWithDelay(1));
        //this.enabled = false;
        active = false;
    }
    IEnumerator EnabledPostFightElementsWithDelay(float delay) {
        yield return new WaitForSeconds(delay);
        if(PostFightElements) PostFightElements.SetActive(true);
    }

}
