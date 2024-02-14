using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

    public static DungeonManager Singleton;
    public SelectableRoomsAsset SelectableRooms;
    [HideInInspector]
    public Room CurrentRoom;

    void Start() {
        Singleton = this;
        CurrentRoom = FindFirstObjectByType<Room>();
    }

    Room GetRandomRoom() { // temp function
        return SelectableRooms.Rooms[Random.Range(0, SelectableRooms.Rooms.Count - 1)];
    }
    public List<Room> GetNewRooms(int count) {
        List<Room> newRooms = new List<Room>();
        for(int i = 0; i < count; i++) {
            newRooms.Add(GetRandomRoom());
        }
        return newRooms;
    }
    public void GoToRoom(Room roomPrefab) {
        DestroyCurrentRoom();
        Room newRoom = Instantiate<Room>(roomPrefab);
        PlayerInput.Singleton.transform.position = newRoom.DoorEnterance.SpawnPoint.transform.position;
    }
    public void DestroyCurrentRoom() {
        if(CurrentRoom)
            Destroy(CurrentRoom.gameObject);
    }
}
