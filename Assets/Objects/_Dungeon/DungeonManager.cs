using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour {

    public static DungeonManager Singleton;
    public SelectableRoomsAsset SelectableRooms;
    public SelectableRoomsAsset DoorChoosingRooms;
    [HideInInspector]
    public Room CurrentRoom;

    private void Awake() {
        Singleton = this;
        CurrentRoom = FindFirstObjectByType<Room>();
    }

    Room GetRandomRoom(SelectableRoomsAsset roomsAsset) {
        return roomsAsset.Rooms[Random.Range(0, roomsAsset.Rooms.Count)];
    }
    public List<Room> GetNewRooms(int count) { // temp function. Don't select the same room more than once per run.
        List<Room> newRooms = new List<Room>();
        for(int i = 0; i < count; i++) {
            newRooms.Add(GetRandomRoom(SelectableRooms));
        }
        return newRooms;
    }
    public void GoToDoorSelectingRoom() {
        GoToRoom(GetRandomRoom(DoorChoosingRooms));
    }
    public void GoToRoom(Room roomPrefab) {
        DestroyCurrentRoom();
        Room newRoom = Instantiate<Room>(roomPrefab);
        CurrentRoom = newRoom;
        PlayerInput.Singleton.transform.position = newRoom.DoorEnterance.SpawnPoint.transform.position;
    }
    public void DestroyCurrentRoom() {
        if(CurrentRoom)
            Destroy(CurrentRoom.gameObject);
    }
    public void RestartDungeon() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
