using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public Door DoorExit;
    public Door DoorEnterance;


    void PlayerReachedExit() {
        DungeonManager.Singleton.GoToDoorSelectingRoom();
    }
    private void OnEnable() {
        if (DoorExit)
            DoorExit.DoorEntered.AddListener(PlayerReachedExit);
    }
    private void OnDisable() {
        if (DoorExit)
            DoorExit.DoorEntered.RemoveListener(PlayerReachedExit);
    }
}
