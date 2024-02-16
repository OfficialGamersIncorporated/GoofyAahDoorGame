using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public Door DoorExit;
    public Door DoorEnterance;
    public List<string> DeathScreenMessages = new List<string>();
    public bool CameraFollowsPlayer = false;

    void PlayerReachedExit() {
        if (!DoorExit.DestinationRoom)
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
