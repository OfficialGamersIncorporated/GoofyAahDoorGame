using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// room logic scripts should be attached to the same object as the room.cs component and inherit from this script.
public class RoomLogicReverse : RoomLogic {

    public float ReverseDelay = 2f;

    new void Start()
    {
        base.Start();
        OpenExitWithDelay(ReverseDelay);
    }

}
