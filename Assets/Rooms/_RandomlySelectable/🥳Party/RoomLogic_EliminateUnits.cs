using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomLogic_EliminateUnits : RoomLogic {

    public List<Health> Units;
    [Tooltip("DEPRICATED use ChallengeCompleted.")]
    public GameObject PostFightElements;
    public UnityEvent ChallengeCompleted;

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
        ChallengeCompleted.Invoke();
        if(PostFightElements) PostFightElements.SetActive(true);
    }

}
