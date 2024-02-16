using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic_OpenDelayed : RoomLogic {

    public float Delay = 5;

    new void Start() {
        base.Start();
        OpenExitWithDelay(Delay);
    }
}
