using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic_Monkey : RoomLogic {
    new IEnumerator Start() {
        base.Start();
        yield return new WaitForSeconds(5);
        OpenExitDoor();
    }
}
