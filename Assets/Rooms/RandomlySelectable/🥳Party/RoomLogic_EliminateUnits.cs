using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic_EliminateUnits : RoomLogic {

    public List<Health> Units;

    private void Update() {
        foreach(Health baby in Units) {
            if(baby != null) return;
        }

        OpenExitWithDelay(1);
        this.enabled = false;
    }

}
