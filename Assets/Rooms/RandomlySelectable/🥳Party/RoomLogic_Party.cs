using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic_Party : RoomLogic {

    public List<Health> Babies;

    private void Update() {
        foreach(Health baby in Babies) {
            if(baby != null) return;
        }

        OpenExitWithDelay(1);
        this.enabled = false;
    }

}
