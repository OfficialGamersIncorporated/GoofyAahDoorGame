using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour {

    public float PlayerEnterRoomSpeed = 10;

    public Room StartRoomPrefab;
    public SelectableRoomsAsset SelectableRooms;
    public SelectableRoomsAsset DoorChoosingRooms;
    public TransitionScreen transitionScreen;

    [HideInInspector]
    public Room CurrentRoom;
    public static DungeonManager Singleton;
    PlayerInput player;

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
    public IEnumerator _EnterRoom(Room existingRoom, float enterSpeed = 0) {
        if (!player)
            player = PlayerInput.Singleton;
        Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
        Door enterance = existingRoom.DoorEnterance;
        if(!enterance) {
            Debug.LogWarning("Room " + existingRoom.name + " doesn't have an enterance door defined.");
            player.gameObject.SetActive(true);
            yield break;
        }

        //float enterSpeed = playerRB.velocity.magnitude;
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
        StartCoroutine(_GoToRoom(roomPrefab));
    }
    IEnumerator _GoToRoom(Room roomPrefab) {
        if(!player)
            player = PlayerInput.Singleton;

        float enterSpeed = player.GetComponent<Rigidbody2D>().velocity.magnitude;
        //print("GOTOROOM player velocity: " + player.GetComponent<Rigidbody2D>().velocity.magnitude);
        player.gameObject.SetActive(false);
        yield return transitionScreen.Show();

        DestroyCurrentRoom();
        Room newRoom = Instantiate<Room>(roomPrefab);
        CurrentRoom = newRoom;

        yield return transitionScreen.Hide();

        yield return _EnterRoom(newRoom, enterSpeed);
    }
    public void DestroyCurrentRoom() {
        if(CurrentRoom)
            Destroy(CurrentRoom.gameObject);
    }
    public void RestartDungeon() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
