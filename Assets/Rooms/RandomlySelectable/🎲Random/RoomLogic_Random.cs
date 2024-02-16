using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomLogic_Random : RoomLogic {

    public TextMeshPro DoorText;
    Room doorRoom;

    new void Start() {
        base.Start();

        doorRoom = DungeonManager.Singleton.GetNewRooms(1)[0];
        DoorText.text = doorRoom.name;
    }
    public void GoToRoom() {
        DungeonManager.Singleton.GoToRoom(doorRoom);
    }
}
