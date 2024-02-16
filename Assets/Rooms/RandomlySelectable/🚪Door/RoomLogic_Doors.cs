using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLogic_Doors : RoomLogic {

    public List<Door> WrongDoors;
    public Room SelfPrefab;

    void ReloadRoom() {
        //DungeonManager.Singleton.GoToRoom(SelfPrefab);
        StartCoroutine(DungeonManager.Singleton._EnterRoom(room));
    }
    new IEnumerator Start() {
        base.Start();

        OpenExitWithDelay(1);

        foreach(Door door in WrongDoors) {
            yield return new WaitForSeconds( (float)Random.Range(10, 50) / 1000f );
            door.Open();
            door.DoorEntered.AddListener(ReloadRoom);
        }
    }
    private void OnDisable() {
        foreach(Door door in WrongDoors) {
            door.DoorEntered.RemoveAllListeners();
        }
    }
    void Update() {

    }
}
