using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic_Bomb : RoomLogic {

    public void ButtonPushed() {
        print("EXPLODE!");
    }

    new private void Start() {
        base.Start();
        OpenExitWithDelay(5);
    }

}
