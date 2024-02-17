using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDoor : MonoBehaviour {

    Door door;

    IEnumerator Start() {
        door = GetComponent<Door>();
        Room destinationRoom = DungeonManager.Singleton.GetNewRooms(1)[0];
        door.SetDestinationRoom(destinationRoom);

        yield return new WaitForSeconds(1);
        door.Open();
    }


    void Update() {

    }
}
