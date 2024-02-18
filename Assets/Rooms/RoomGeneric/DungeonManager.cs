using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class DungeonManager : MonoBehaviour {

    public bool CollectableBottle = false;
    public bool CollectableKeyRing = false;
    public bool CollectableStuffedBear = false;

    public float PlayerEnterRoomSpeed = 10;
    public enum VisitCounterType { Entered, Seen };
    public VisitCounterType VisitCountMode = VisitCounterType.Entered;

    public Room StartRoomPrefab;
    public SelectableRoomsAsset SelectableRooms;
    public SelectableRoomsAsset AllowMultipleVisitsRooms;
    public SelectableRoomsAsset DoorChoosingRooms;
    public Room DoorChoosingRoom_Final;
    public TransitionScreen transitionScreen;

    public UnityEvent CollectableAquired;

    [HideInInspector]
    public Room CurrentRoom;
    public static DungeonManager Singleton;
    PlayerInput player;

    public List<Room> VisitedRooms = new List<Room>();
    public int roomNumber = 0;

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

    bool ListContainsRoom(Room querry, List<Room> haystack) {
        return haystack.Contains(querry);
        //foreach(Room other in haystack) {
        //    // For some reason just comparing prefab references isn't consistent. // just kidding I was using the function that didn't check.
        //    if(other.name == querry.name) return true;
        //}
        //return false;
    }
    Room GetRandomRoom(SelectableRoomsAsset roomsAsset) {
        return roomsAsset.Rooms[Random.Range(0, roomsAsset.Rooms.Count)];
    }
    Room GetRandomUniqueRoom(SelectableRoomsAsset roomsAsset) {
        Room selectedRoom = roomsAsset.Rooms[Random.Range(0, roomsAsset.Rooms.Count)];

        if(!ListContainsRoom(selectedRoom, AllowMultipleVisitsRooms.Rooms)) {
            if(ListContainsRoom(selectedRoom, VisitedRooms))
                return GetRandomUniqueRoom(roomsAsset);
            else if(VisitCountMode == VisitCounterType.Seen) {
                print("Visited rooms does not contain " + selectedRoom.name);
                VisitedRooms.Add(selectedRoom);
            }
        }

        return selectedRoom;
    }
    public List<Room> GetRandomRooms(int count) {
        List<Room> newRooms = new List<Room>();
        for(int i = 0; i < count; i++) {
            newRooms.Add(GetRandomRoom(SelectableRooms));
        }
        return newRooms;
    }
    public List<Room> GetNewRooms(int count) {
        List<Room> newRooms = new List<Room>();
        for(int i = 0; i < count; i++) {
            newRooms.Add(GetRandomUniqueRoom(SelectableRooms));
        }
        return newRooms;
    }
    public void GoToDoorSelectingRoom() {
        roomNumber++;
        if(CollectableBottle && CollectableKeyRing && CollectableStuffedBear)
            GoToRoom(DoorChoosingRoom_Final);
        else
            GoToRoom(GetRandomRoom(DoorChoosingRooms));
    }
    public IEnumerator _EnterRoom(Room existingRoom, float enterSpeed = 0) {
        if (!player)
            player = PlayerInput.Singleton;
        Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
        Door enterance = existingRoom.DoorEnterance;
        if(!enterance) {
            //Debug.LogWarning("Room " + existingRoom.name + " doesn't have an enterance door defined.");
            player.gameObject.SetActive(true);
            yield break;
        }

        //float enterSpeed = playerRB.velocity.magnitude;
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
        if (VisitCountMode == VisitCounterType.Entered) {
            if (!ListContainsRoom(roomPrefab, AllowMultipleVisitsRooms.Rooms) )// && !VisitedRooms.Contains(roomPrefab))
                VisitedRooms.Add(roomPrefab);
        }

        if(!player)
            player = PlayerInput.Singleton;

        float enterSpeed = player.GetComponent<Rigidbody2D>().velocity.magnitude;
        //print("GOTOROOM player velocity: " + player.GetComponent<Rigidbody2D>().velocity.magnitude);
        player.gameObject.SetActive(false);

        yield return transitionScreen.Show();

        // we move the player before they're enabled because the camera snaps to the
        // player's postion in rooms with CameraFollowsPlayer, even when the player is disabled.
        Door destinationEnterance = roomPrefab.DoorEnterance;
        if (destinationEnterance)
            player.transform.position = roomPrefab.DoorEnterance.SpawnPoint.transform.position;

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
    public void GiveBossCollectable(Interactable_Collectable.BossCollectableType collectable) {
        if(collectable == Interactable_Collectable.BossCollectableType.BabyBottle)
            CollectableBottle = true;
        else if(collectable == Interactable_Collectable.BossCollectableType.KeyRing)
            CollectableKeyRing = true;
        else if(collectable == Interactable_Collectable.BossCollectableType.StuffedBear)
            CollectableStuffedBear = true;

        CollectableAquired.Invoke();
    }
}
