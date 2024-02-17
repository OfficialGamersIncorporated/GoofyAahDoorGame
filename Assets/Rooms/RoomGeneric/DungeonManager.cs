using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour {

    public static DungeonManager Singleton;
    public Room StartRoomPrefab;
    public SelectableRoomsAsset SelectableRooms;
    public SelectableRoomsAsset DoorChoosingRooms;
    public float PlayerEnterRoomSpeed = 10;
    [HideInInspector]
    public Room CurrentRoom;

    private void Awake() {
        Singleton = this;
        CurrentRoom = FindFirstObjectByType<Room>();
    }
    private void Start() {
        if(CurrentRoom)
            StartCoroutine(_EnterRoom(CurrentRoom));
        if(!CurrentRoom)
            GoToRoom(StartRoomPrefab);
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
    public IEnumerator _EnterRoom(Room existingRoom) {
        PlayerInput player = PlayerInput.Singleton;
        Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
        Door enterance = existingRoom.DoorEnterance;
        if(!enterance) {
            Debug.LogWarning("Room " + existingRoom.name + " doesn't have an enterance door defined.");
            yield break;
        }

        float enterSpeed = playerRB.velocity.magnitude;
        print(enterSpeed);

        player.gameObject.SetActive(false);
        // we move the player before they're enabled because the camera snaps to the
        // player's postion in rooms with CameraFollowsPlayer, even when the player is disabled.
        player.transform.position = enterance.SpawnPoint.transform.position;

        yield return new WaitForSeconds(.5f);
        enterance.Open();
        yield return new WaitForSeconds(.5f);
        player.gameObject.SetActive(true);

        playerRB.velocity = enterance.FacingNormal * Mathf.Max(PlayerEnterRoomSpeed, enterSpeed);

        yield return new WaitForSeconds(.25f);
        enterance.Close();
    }
    public void GoToRoom(Room roomPrefab) {
        DestroyCurrentRoom();
        Room newRoom = Instantiate<Room>(roomPrefab);
        CurrentRoom = newRoom;

        StartCoroutine(_EnterRoom(newRoom));
    }
    public void DestroyCurrentRoom() {
        if(CurrentRoom)
            Destroy(CurrentRoom.gameObject);
    }
    public void RestartDungeon() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
