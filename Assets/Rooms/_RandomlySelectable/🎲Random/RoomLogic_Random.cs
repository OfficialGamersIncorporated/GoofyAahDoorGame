using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomLogic_Random : RoomLogic {

    public TextMeshPro DoorText;
    Room doorRoom;

    new void Start() {
        base.Start();

        doorRoom = DungeonManager.Singleton.GetRandomRooms(1)[0];
        //DoorText.text = doorRoom.name;
        room.DoorExit.SetDestinationRoom(doorRoom);

        OpenExitWithDelay(1);
    }
    //public void GoToRoom() {
    //    DungeonManager.Singleton.GoToRoom(doorRoom);
    //}
}
